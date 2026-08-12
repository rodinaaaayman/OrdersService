namespace OrdersService.Application.IntegrationMessages;
public class ClientStatusResponse
{
    public int Id { get; set; }
    public bool Exists { get; set; }
    public bool IsActive { get; set; }
    public decimal AccountBalance { get; set; }
    public bool FundsReserved { get; set; }
    public string? FailureReason { get; set; }
}