using MediatR;
using OrdersService.Application.IntegrationEvents;
using OrdersService.Domain.Enums;
using OrdersService.Domain.Models;
using OrdersService.Application.Interfaces;
using System.Text.Json;

namespace OrdersService.Application.Services.orders.Commands.FillOrder;

public class FillOrderCommandHandler : IRequestHandler<FillOrderCommand>
{
    private readonly IApplicationDbContext _context;

    public FillOrderCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(FillOrderCommand request, CancellationToken ct)
    {
        var order = await _context.Orders.FindAsync(new object[] { request.OrderId }, ct);
        order.Status = OrderStatus.Filled;

        var invoice = new Invoices
        {
            OrderId = order.OrderId,
            NetAmount = order.NetAmount,
            Commission = order.Commission,
            GrossAmount = order.GrossAmount,
            InvoiceDate = DateTime.Now
        };
        _context.Invoices.Add(invoice);

        var integrationEvent = new OrderFilledEvent
        {
            OrderId = order.OrderId,
            Id = order.Id,
            Quantity = order.Quantity,
            UnitPrice = order.UnitPrice,
            NetAmount = order.NetAmount,
            Commission = order.Commission,
            GrossAmount = order.GrossAmount,
            InvoiceDate = DateTime.UtcNow
        };

        _context.OutboxMessages.Add(new OutboxMessage
        {
            Id = Guid.NewGuid(),
            Type = nameof(OrderFilledEvent),
            Payload = JsonSerializer.Serialize(integrationEvent),
            OccurredAt = DateTime.UtcNow
        });

        await _context.SaveChangesAsync(ct);
    }
}