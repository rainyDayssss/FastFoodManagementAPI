using FastFoodManagementAPI.Features.Sales;
using System.Text.Json.Serialization;

namespace FastFoodManagementAPI.Features.Orders
{
    public class Order
    {
        public int Id { get; set; }
        public int? TableId { get; set; } // optional for takeout
        public List<OrderItem> OrderItems { get; set; } = new();
        public OrderStatus OrderStatus { get; set; }
        [JsonIgnore]
        public Payment? Payment { get; set; } // nullable, because order may not have a sale yet
    }
}
