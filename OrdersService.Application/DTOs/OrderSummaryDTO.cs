namespace OrdersService.Application.DTOs
{
    public class OrderSummaryDTO
    {
        public int OrderId { get; set; }
        public string OrderType { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public string Status { get; set; }
    }
}