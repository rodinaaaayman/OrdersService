using OrdersService.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OrdersService.Domain.Enums;

namespace OrdersService.Application.Services.orders.Commands.CancelOrder;

public class CancelOrderCommandHandler
    : IRequestHandler<CancelOrderCommand, CancelOrderResult>
{
    private readonly IApplicationDbContext _context;

    public CancelOrderCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<CancelOrderResult> Handle(
        CancelOrderCommand request,
        CancellationToken cancellationToken)
    {
        var order = await _context.Orders
            .FirstOrDefaultAsync(
                o => o.OrderId == request.OrderId,
                cancellationToken);

        if (order == null)
        {
            return new CancelOrderResult { Success = false, Message = "Order not found." };
        }

        if (order.Status == OrderStatus.Filled)
        {
            return new CancelOrderResult
            {
                Success = false,
                Message = "This order is already filled and cannot be deleted."
            };
        }
        if (order.Status == OrderStatus.PartiallyFilled)
        {
            return new CancelOrderResult
            {
                Success = false,
                Message = "This order is already partially filled and cannot be deleted."
            };
        }
        if (order.Status == OrderStatus.Cancelled)
        {
            return new CancelOrderResult
            {
                Success = false,
                Message = "This order is already cancelled."
            };
        }
        order.Status = OrderStatus.Cancelled;
        await _context.SaveChangesAsync(cancellationToken);

        return new CancelOrderResult { Success = true };
    }
}