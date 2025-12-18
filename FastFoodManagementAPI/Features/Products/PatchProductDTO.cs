using FastFoodManagementAPI.Features.Orders;
using System.Text.Json.Serialization;

namespace FastFoodManagementAPI.Features.Products
{
    public class PatchProductDTO
    {
        public string? Name { get; set; }
        public string? Description { get; set; } 
        public decimal? Price { get; set; }
        public int? Stock { get; set; }
        public string? ImagePath { get; set; } 
    }
}
