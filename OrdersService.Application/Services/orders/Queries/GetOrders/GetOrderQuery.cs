using OrdersService.Application.DTOs;
using MediatR;

namespace OrdersService.Application.Services.orders.Queries.GetOrders;
    public record GetOrdersQuery(
    int? Cursor,
    int Limit = 20)
    : IRequest<List<OrdersDTO>>;

