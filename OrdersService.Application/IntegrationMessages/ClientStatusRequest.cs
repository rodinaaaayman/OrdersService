namespace OrdersService.Application.IntegrationMessages;
public class ClientStatusRequest
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
}