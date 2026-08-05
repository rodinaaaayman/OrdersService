using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace OrdersService.Domain.Models
{
    public class Executions
    {
        public int ExecutionId { get; set; }
        public int OrderId { get; set; }
        [JsonIgnore]
        public Orders Order { get; set; } = null!;
        public int ExecutionQuantity { get; set; }

        public DateTime ExecutionDate { get; set; }
    }
}
