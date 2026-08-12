using OrdersService.Domain.Enums;

namespace OrdersService.Application.Services.orders.Commands.PlaceOrder;

public record PlaceOrderResult(int OrderId, OrderTypes OrderType, decimal? LimitPrice, decimal UnitPrice, int Quantity, decimal GrossAmount);