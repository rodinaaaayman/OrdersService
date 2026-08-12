using MediatR;
using OrdersService.Application.Abstractions;
using OrdersService.Application.Interfaces;
using OrdersService.Domain.Models;

namespace OrdersService.Application.Services.orders.Commands.PlaceOrder;
public class PlaceOrderCommandHandler : IRequestHandler<PlaceOrderCommand, PlaceOrderResult>
{
    private readonly IApplicationDbContext _context;
    private readonly IClientVerificationService _clientVerification;

    public PlaceOrderCommandHandler(IApplicationDbContext context, IClientVerificationService clientVerification)
    {
        _context = context;
        _clientVerification = clientVerification;
    }

    public async Task<PlaceOrderResult> Handle(PlaceOrderCommand request, CancellationToken cancellationToken)
    {
        var order = Orders.Create(
            clientId: request.Id,
            orderType: request.OrderType,
            limitPrice: request.LimitPrice,
            unitPrice: request.UnitPrice,
            quantity: request.Quantity,
            commissionRate: request.CommissionRate
        );

        var status = await _clientVerification.GetClientStatusAsync(request.Id, order.GrossAmount, cancellationToken);

        if (!status.Exists)
            throw new InvalidOperationException($"Client {request.Id} does not exist.");
        if (!status.IsActive)
            throw new InvalidOperationException($"Client {request.Id} is not active.");
        if (!status.FundsReserved)
            throw new InvalidOperationException(status.FailureReason ?? $"Could not reserve funds for client {request.Id}.");

        _context.Orders.Add(order);
        await _context.SaveChangesAsync(cancellationToken);

        return new PlaceOrderResult(order.OrderId, order.OrderType, order.LimitPrice, order.UnitPrice, order.Quantity, order.GrossAmount);
    }
}