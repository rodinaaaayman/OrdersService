using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using OrdersService.Domain.Enums;
 

namespace OrdersService.Domain.Models
{
    public class Orders
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderId { get; set; }

        [ForeignKey("Client")]
        public int Id { get; set; }
        public OrderTypes OrderType { get; set; } = OrderTypes.Market;
        
        public decimal LimitPrice { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        [Required]
        public decimal UnitPrice { get; set; }

        [Required]
        public int Quantity { get; set; }
        public int FilledQuantity { get; set; } = 0;
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public decimal NetAmount => UnitPrice*Quantity;

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public decimal Commission => CommissionRate*NetAmount;

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public decimal GrossAmount => NetAmount + Commission;
        public decimal CommissionRate { get; set; } = 0.005m;
        
        [StringLength(50)]
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        
        public ICollection<Executions> Executions { get; set; } = new List<Executions>();
        [JsonIgnore]
        public Invoice? Invoice { get; set; }
    }
}
