using MassTransit;
using MediatR;
using OrdersService.Application.Events;
using OrdersService.Application.IntegrationEvents;
using OrdersService.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace OrdersService.Application.Services.orders
{
    public class OrderFilledIntegrationEventHandler : INotificationHandler<OrderFullyFilledEvent>
    {
        private readonly IApplicationDbContext _context;
        private readonly IPublishEndpoint _publishEndpoint;

        public OrderFilledIntegrationEventHandler(IApplicationDbContext context, IPublishEndpoint publishEndpoint)
        {
            _context = context;
            _publishEndpoint = publishEndpoint;
        }

        public async Task Handle(OrderFullyFilledEvent notification, CancellationToken cancellationToken)
        {
            var order = await _context.Orders
                .FirstOrDefaultAsync(o => o.OrderId == notification.OrderId, cancellationToken);

            if (order == null) return;

            await _publishEndpoint.Publish(new OrderFilledEvent
            {
                OrderId = order.OrderId,
                Id = order.Id,
                Quantity = order.Quantity,
                UnitPrice = order.UnitPrice,
                GrossAmount = order.GrossAmount,
                InvoiceDate = DateTime.UtcNow
            }, cancellationToken);
        }
    }
}
