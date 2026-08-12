using  OrdersService.Domain.Enums;

namespace OrdersService.Application.DTOs;
    public class CreateOrdersDTO
    {
        public int Id { get; set; }
        public OrderTypes OrderType { get; set; }
        public decimal LimitPrice { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal GrossAmount { get; set; }
    }

