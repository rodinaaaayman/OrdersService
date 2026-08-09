using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace OrdersService.Domain.Models
{
    public class Executions
    {
        [Key]
        public int ExecutionId { get; set; }
        public int OrderId { get; set; }
        [JsonIgnore]
        public Orders Order { get; set; } = null!;
        public int ExecutionQuantity { get; set; }

        public DateTime ExecutionDate { get; set; }
    }
}
