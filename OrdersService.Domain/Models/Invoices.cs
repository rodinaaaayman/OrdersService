using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace OrdersService.Domain.Models
{
    public class Invoice
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int InvoiceId { get; set; }

        [ForeignKey("Order")]
        public int OrderId { get; set; }

        [JsonIgnore]
        public Orders Order { get; set; } = null!;

        public decimal TradeValue { get; set; }

        public decimal Commission { get; set; }

        public decimal Tax { get; set; }

        public decimal Total { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}