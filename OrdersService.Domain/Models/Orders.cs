using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using OrdersService.Domain.Enums;
 

namespace OrdersService.Domain.Models
{
    public class Orders
    {
        public int OrderId { get; set; }

        //[ForeignKey("Client")]
        //public int Id { get; set; }
        public OrderTypes OrderType { get; set; } = OrderTypes.Market;
        
        public decimal LimitPrice { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public int FilledQuantity { get; set; } = 0;
        public decimal NetAmount { get; set; }
        public decimal Commission { get; set; }
        public decimal GrossAmount { get; set; }
        public decimal CommissionRate { get; set; } = 0.005m;
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public ICollection<Executions> Executions { get; set; } = new List<Executions>();
        [JsonIgnore]
        public Invoices? Invoice { get; set; }
    }
}
