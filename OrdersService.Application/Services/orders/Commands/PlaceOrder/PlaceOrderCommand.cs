using MediatR;
using OrdersService.Domain.Enums;

namespace OrdersService.Application.Services.orders.Commands.PlaceOrder;

public class PlaceOrderCommand : IRequest<int>
{
    public int Id { get; set; }
    public int OrderId { get; set; }

    public OrderTypes OrderType { get; set; }

    public decimal LimitPrice { get; set; }

    public int UnitPrice { get; set; }

    public int Quantity { get; set; }
}