using OrdersService.Domain.Models;
using MediatR;

public class GetOrderByIdQuery : IRequest<Orders>
{
    public int OrderId { get; set; }

    public GetOrderByIdQuery(int orderId)
    {
       OrderId = orderId;
    }
}