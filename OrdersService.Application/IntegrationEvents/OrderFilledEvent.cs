namespace OrdersService.Application.IntegrationEvents
{
    public class OrderFilledEvent
    {
        public int InvoiceId { get; set; }
        public int OrderId { get; set; }
        public int Id { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal NetAmount { get; set; }
        public decimal Commission { get; set; }
        public decimal GrossAmount { get; set; }
        public DateTime InvoiceDate { get; set; }
    }
}

