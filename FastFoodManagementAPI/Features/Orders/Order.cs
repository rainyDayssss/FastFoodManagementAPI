using System.Text.Json.Serialization;

namespace FastFoodManagementAPI.Features.Orders;

public class Order
{
    public int Id { get; set; }
    public int? TableId { get; set; } // optional for takeout
    public List<OrderItem> OrderItems { get; set; } = new();
    public OrderStatus OrderStatus { get; set; } = OrderStatus.Confirmed; // default
    public decimal Total => OrderItems.Sum(i => i.LineTotal); // total for order
}
