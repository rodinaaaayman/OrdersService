using MediatR;
using OrdersService.Domain.Enums;

namespace OrdersService.Application.Services.orders.Commands.PlaceOrder;

public class PlaceOrderCommand : IRequest<PlaceOrderResult>
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public OrderTypes OrderType { get; set; }
    public decimal LimitPrice { get; set; }
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }
    public decimal CommissionRate { get; set; } = 0.005m;
    public decimal Commission { get; set; }
    public decimal GrossAmount { get; set; }
}