namespace OrdersService.Application.Services.orders.Commands.CancelOrder;

public class CancelOrderResult
{
    public bool Success { get; set; }
    public string? Message { get; set; }
}