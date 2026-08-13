using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace OrdersService.Domain.Models
{
    public class Invoices
    {
        [Key]
        public int InvoiceId { get; set; }
        public int OrderId { get; set; }
        [JsonIgnore]
        public Orders Order { get; set; } = null!;
        public decimal NetAmount { get; set; }
        public decimal Commission { get; set; }
        //public decimal CommissionRate { get; set; } = 0.005m;
        public decimal GrossAmount { get; set; }
        public DateTime InvoiceDate { get; set; } = DateTime.Now;
    }
}