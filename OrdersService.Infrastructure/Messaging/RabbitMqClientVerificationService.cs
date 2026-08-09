using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Collections.Concurrent;
using System.Text;
using System.Text.Json;
using OrdersService.Application.Abstractions;
using OrdersService.Application.IntegrationMessages;


public class RabbitMqClientVerificationService : IClientVerificationService, IAsyncDisposable
{
    private readonly IConfiguration _config;
    private IConnection? _connection;
    private IChannel? _channel;
    private string? _replyQueueName;
    private readonly ConcurrentDictionary<string, TaskCompletionSource<ClientStatusResponse>> _pendingRequests = new();
    private readonly SemaphoreSlim _initLock = new(1, 1);
    private const string RequestQueueName = "client_status_requests";

    public RabbitMqClientVerificationService(IConfiguration config)
    {
        _config = config;
    }

    private async Task EnsureInitializedAsync()
    {
        if (_channel is not null) return;

        await _initLock.WaitAsync();
        try
        {
            if (_channel is not null) return;

            var factory = new ConnectionFactory
            {
                HostName = _config["RabbitMq:HostName"],
                Port = int.Parse(_config["RabbitMq:Port"]!),
                UserName = _config["RabbitMq:UserName"],
                Password = _config["RabbitMq:Password"]
            };

            _connection = await factory.CreateConnectionAsync();
            _channel = await _connection.CreateChannelAsync();

            await _channel.QueueDeclareAsync(RequestQueueName, durable: true, exclusive: false, autoDelete: false);

            var replyQueue = await _channel.QueueDeclareAsync(queue: "", exclusive: true, autoDelete: true);
            _replyQueueName = replyQueue.QueueName;

            var consumer = new AsyncEventingBasicConsumer(_channel);
            consumer.ReceivedAsync += OnReplyReceivedAsync;

            await _channel.BasicConsumeAsync(_replyQueueName, autoAck: true, consumer: consumer);
        }
        finally
        {
            _initLock.Release();
        }
    }

    private Task OnReplyReceivedAsync(object sender, BasicDeliverEventArgs ea)
    {
        var correlationId = ea.BasicProperties.CorrelationId;

        if (correlationId is not null && _pendingRequests.TryRemove(correlationId, out var tcs))
        {
            var json = Encoding.UTF8.GetString(ea.Body.ToArray());
            var response = JsonSerializer.Deserialize<ClientStatusResponse>(json);
            tcs.TrySetResult(response!);
        }

        return Task.CompletedTask;
    }

    public async Task<ClientStatusResponse> GetClientStatusAsync(int Id, CancellationToken ct = default)
    {
        await EnsureInitializedAsync();

        var correlationId = Guid.NewGuid().ToString();
        var tcs = new TaskCompletionSource<ClientStatusResponse>(TaskCreationOptions.RunContinuationsAsynchronously);
        _pendingRequests[correlationId] = tcs;

        var json = JsonSerializer.Serialize(new ClientStatusRequest { Id = Id });
        var body = Encoding.UTF8.GetBytes(json);

        var props = new BasicProperties
        {
            CorrelationId = correlationId,
            ReplyTo = _replyQueueName
        };

        await _channel!.BasicPublishAsync(
            exchange: "",
            routingKey: RequestQueueName,
            mandatory: false,
            basicProperties: props,
            body: body);

        var completedTask = await Task.WhenAny(tcs.Task, Task.Delay(TimeSpan.FromSeconds(5), ct));

        if (completedTask != tcs.Task)
        {
            _pendingRequests.TryRemove(correlationId, out _);
            throw new TimeoutException($"AuthUserService did not respond within 5 seconds for client {Id}.");
        }

        return await tcs.Task;
    }

    public async ValueTask DisposeAsync()
    {
        if (_channel is not null) await _channel.DisposeAsync();
        if (_connection is not null) await _connection.DisposeAsync();
    }
}