namespace OrdersService.Application.DTOs
{
    public class OrdersDTO
    {
        public int OrderId { get; set; }

        public decimal UnitPrice { get; set; }
        public decimal Quantity { get; set; }

        public int Id { get; set; }
    }
}
