using OrdersService.Application.IntegrationMessages;
namespace OrdersService.Application.Abstractions;

public interface IClientVerificationService
{
    Task<ClientStatusResponse> GetClientStatusAsync(int Id, CancellationToken ct = default);
}