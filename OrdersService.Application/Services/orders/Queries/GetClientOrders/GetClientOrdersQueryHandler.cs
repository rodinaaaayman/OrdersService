using OrdersService.Application.Interfaces;
using OrdersService.Application.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace OrdersService.Application.Services.orders.Queries.GetClientOrders
{
    public class GetClientOrdersHandler : IRequestHandler<GetClientOrdersQuery, List<OrderSummaryDTO>>
    {
        private readonly IApplicationDbContext _context;

        public GetClientOrdersHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<OrderSummaryDTO>> Handle(
            GetClientOrdersQuery request,
            CancellationToken cancellationToken)
        {
            var orders = await _context.Orders
                .Where(o => o.Id == request.Id)
                .Select(o => new OrderSummaryDTO
                {
                    OrderId = o.OrderId,
                    OrderType = o.OrderType.ToString(),
                    Quantity = o.Quantity,
                    UnitPrice = o.UnitPrice,
                    Status = o.Status.ToString(),
                })
                .ToListAsync(cancellationToken);

            return orders;
        }
    }
}