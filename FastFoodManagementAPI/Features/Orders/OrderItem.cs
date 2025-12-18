    using FastFoodManagementAPI.Features.Products;
    using System.Text.Json.Serialization;

    namespace FastFoodManagementAPI.Features.Orders
    {
        public class OrderItem
        {
            public int Id { get; set; }
        
            public int OrderId { get; set; } 
            [JsonIgnore]
            public Order Order { get; set; } = null!;
        
            public int ProductId { get; set; }
            [JsonIgnore]
            public Product Product { get; set; } = null!;
        
            public decimal UnitPrice { get; set; }
            public int Quantity { get; set; }
        }
    }
