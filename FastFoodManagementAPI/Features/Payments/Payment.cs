using FastFoodManagementAPI.Features.Orders;
using System.Text.Json.Serialization;

namespace FastFoodManagementAPI.Features.Sales
{
    public class Payment
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        [JsonIgnore]
        public Order Order { get; set; } = null!;
        public decimal TotalAmount { get; set; }
    }
}
