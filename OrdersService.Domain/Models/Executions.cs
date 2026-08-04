using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace OrdersService.Domain.Models
{
    public class Executions
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ExecutionId { get; set; }
        [ForeignKey("Orders")]
        public int OrderId { get; set; }
        [Required]
        [JsonIgnore]
        public Orders Order { get; set; } = null!;
        [Required]
        public int ExecutionQuantity { get; set; }

        public DateTime ExecutionDate { get; set; }
    }
}
