using OrdersService.Application.Interfaces;
using OrdersService.Application.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace OrdersService.Application.Services.orders.Queries.GetOrders;
public class GetOrdersQueryHandler
    : IRequestHandler<GetOrdersQuery, List <OrdersDTO>>
{
    private readonly IApplicationDbContext _context;

    public GetOrdersQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<List<OrdersDTO>> Handle(
    GetOrdersQuery request,
    CancellationToken cancellationToken)
    {
        var query = _context.Orders
            .OrderBy(o => o.OrderId)
            .AsQueryable();

        if (request.Cursor.HasValue)
        {
            query = query.Where(o => o.OrderId > request.Cursor.Value);
        }

        var orders = await query
            .Take(request.Limit)
            .Select(o => new OrdersDTO
            {
                OrderId = o.OrderId,
                Quantity = o.Quantity,
                UnitPrice = o.UnitPrice,
                Id = o.Id
            })
            .ToListAsync(cancellationToken);

        return orders;
    }
}
