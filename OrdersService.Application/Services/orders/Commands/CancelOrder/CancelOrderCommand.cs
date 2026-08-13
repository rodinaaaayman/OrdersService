using MediatR;

namespace OrdersService.Application.Services.orders.Commands.CancelOrder;

public class CancelOrderCommand : IRequest<CancelOrderResult>
{
    public int OrderId { get; set; }
}