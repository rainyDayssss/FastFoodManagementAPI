using FastFoodManagementAPI.Features.Orders;
using System.Text.Json.Serialization;

namespace FastFoodManagementAPI.Features.Products
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public bool IsActive { get; set; } = true;
        public string ImagePath { get; set; } = null!;
        [JsonIgnore]
        public List<OrderItem> OrderItems { get; set; } = new();
    }
}
