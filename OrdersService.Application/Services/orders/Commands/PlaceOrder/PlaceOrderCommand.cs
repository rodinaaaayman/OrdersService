using MediatR;
using OrdersService.Domain.Enums;
using OrdersService.Domain.Models;

namespace OrdersService.Application.Services.orders.Commands.PlaceOrder;

public class PlaceOrderCommand : IRequest<Orders>
{
    public int OrderId { get; set; }

    public OrderTypes OrderType { get; set; }

    public decimal LimitPrice { get; set; }

    public int UnitPrice { get; set; }

    public int Quantity { get; set; }
}