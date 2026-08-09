using MediatR;

namespace OrdersService.Application.Services.orders.Commands.FillOrder;

public class FillOrderCommand : IRequest
{
    public int OrderId { get; set; }

    public FillOrderCommand(int orderId)
    {
        OrderId = orderId;
    }
}