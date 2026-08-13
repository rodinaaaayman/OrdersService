using OrdersService.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace OrdersService.Domain.Models
{
    public class Orders
    {
        [Key]
        public int OrderId { get; set; }
        public int Id { get; set; } //Client.Id
        public OrderTypes OrderType { get; set; } = OrderTypes.Market;
        public decimal LimitPrice { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public int FilledQuantity { get; set; } = 0;
        public decimal NetAmount { get; private set; }
        public decimal Commission { get; private set; }
        public decimal GrossAmount { get; private set; }
        private decimal _commissionRate = 0.005m;
        public decimal CommissionRate
        {
            get => _commissionRate;
            set => _commissionRate = (value == 0) ? 0.005m : value;
        }
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public ICollection<Executions> Executions { get; set; } = new List<Executions>();
        [JsonIgnore]
        public Invoices? Invoice { get; set; }
        private Orders() { }

        public static Orders Create(int clientId, OrderTypes orderType, decimal limitPrice, decimal unitPrice, int quantity, decimal commissionRate)
        {
            if (unitPrice <= 0) throw new ArgumentException("Unit price must be positive.");
            if (quantity <= 0) throw new ArgumentException("Quantity must be more than 0.");
            if (commissionRate == 0) commissionRate = 0.005m;

            var netAmount = quantity * unitPrice;
            var commission = netAmount * commissionRate;
            var grossAmount = netAmount + commission;

            return new Orders
            {
                Id = clientId,
                OrderType = orderType,
                LimitPrice = limitPrice,
                UnitPrice = unitPrice,
                Quantity = quantity,
                NetAmount = netAmount,
                Commission = commission,
                GrossAmount = grossAmount,
                CommissionRate = commissionRate
            };
        }
    }
}