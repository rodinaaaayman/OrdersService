using OrdersService.Application.IntegrationMessages;

namespace OrdersService.Application.Abstractions;
public interface IClientVerificationService
{
    Task<ClientStatusResponse> GetClientStatusAsync(int Id, decimal grossamount, CancellationToken ct = default);
}