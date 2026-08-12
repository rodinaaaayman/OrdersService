using OrdersService.Application.Events;
using OrdersService.Application.Interfaces;
using OrdersService.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace OrdersService.Application.Services.invoices
{
    public class InvoiceGenerationHandler
        : INotificationHandler<OrderFullyFilledEvent>
    {
        private readonly IApplicationDbContext _context;
        public InvoiceGenerationHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task Handle(
            OrderFullyFilledEvent notification,
            CancellationToken cancellationToken)
        {
            var order = await _context.Orders
                .FirstOrDefaultAsync(
                    o => o.OrderId == notification.OrderId,
                    cancellationToken);
            if (order == null)
                return;

            var exists = await _context.Invoices
                .AnyAsync(
                    i => i.OrderId == order.OrderId,
                    cancellationToken);
            if (exists)
                return;
            var NetAmount = order.Quantity * order.UnitPrice;
            var commission = NetAmount * order.CommissionRate;
            var invoice = new Invoices
            {
                OrderId = order.OrderId,
                NetAmount = NetAmount,
                Commission = commission,
                GrossAmount = NetAmount + commission,
                InvoiceDate = DateTime.UtcNow
            };


            _context.Invoices.Add(invoice);

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
