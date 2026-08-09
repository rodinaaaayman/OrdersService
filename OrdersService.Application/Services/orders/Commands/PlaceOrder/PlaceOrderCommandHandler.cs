using MediatR;
using OrdersService.Application.Interfaces;
using OrdersService.Domain.Models;
using OrdersService.Application.Services.orders.Commands.PlaceOrder;
namespace OrdersService.Application.Abstractions;

public class PlaceOrderCommandHandler : IRequestHandler<PlaceOrderCommand, int>
{
    private readonly IApplicationDbContext _context;
    private readonly IClientVerificationService _clientVerification;

    public PlaceOrderCommandHandler(IApplicationDbContext context, IClientVerificationService clientVerification)
    {
        _context = context;
        _clientVerification = clientVerification;
    }

    public async Task<int> Handle(PlaceOrderCommand request, CancellationToken cancellationToken)
    {
        var status = await _clientVerification.GetClientStatusAsync(request.Id, cancellationToken);

        if (!status.Exists)
            throw new InvalidOperationException($"Client {request.Id} does not exist.");

        if (!status.IsActive)
            throw new InvalidOperationException($"Client {request.Id} is not active.");

        var order = new Orders
        {
            Id = request.Id,
            OrderId = request.OrderId,
            OrderType = request.OrderType,
            LimitPrice = request.LimitPrice,
            UnitPrice = request.UnitPrice,
            Quantity = request.Quantity
        };

        _context.Orders.Add(order);
        await _context.SaveChangesAsync(cancellationToken);

        return order.OrderId;
    }
}