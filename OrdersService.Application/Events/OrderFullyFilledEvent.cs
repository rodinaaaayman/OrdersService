using MediatR;

namespace OrdersService.Application.Events
{
    public class OrderFullyFilledEvent : INotification
    {
        public int OrderId { get; }
        public OrderFullyFilledEvent(int orderId)
        {
            OrderId = orderId;
        }
    }
}
