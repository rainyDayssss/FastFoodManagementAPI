namespace FastFoodManagementAPI.Features.Products
{
    public class CreateProductDTO
    {
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public int Stock { get; set; }
    }
}
