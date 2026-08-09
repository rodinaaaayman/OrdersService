using OrdersService.Application.DTOs;
using MediatR;

namespace OrdersService.Application.Services.orders.Queries.GetClientOrders
{
    public record GetClientOrdersQuery(int Id) : IRequest<List<OrderSummaryDTO>>;
}