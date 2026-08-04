using MediatR;

namespace OrdersService.Application.Services.orders.Commands.CancelOrder;

    public class CancelOrderCommand : IRequest<bool>
    {
        public int OrderId { get; set; }

        public CancelOrderCommand(int orderId)
        {
            OrderId = orderId;
        }
    }

