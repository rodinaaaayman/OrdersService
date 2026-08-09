using MassTransit;
using Microsoft.Extensions.Logging;
using OrdersService.Application.IntegrationEvents;

namespace OrdersService.Application.Services.orders
{
    public class OrderFilledConsumer : IConsumer<OrderFilledEvent>
    {
        private readonly ILogger<OrderFilledConsumer> _logger;

        public OrderFilledConsumer(ILogger<OrderFilledConsumer> logger)
        {
            _logger = logger;
        }

        public Task Consume(ConsumeContext<OrderFilledEvent> context)
        {
            var evt = context.Message;

            _logger.LogInformation(
                "Order {OrderId} filled for client {Id}: {Quantity} @ {UnitPrice}, gross {GrossAmount}",
                evt.OrderId, evt.Id, evt.Quantity, evt.UnitPrice, evt.GrossAmount);

            return Task.CompletedTask;
        }
    }
}