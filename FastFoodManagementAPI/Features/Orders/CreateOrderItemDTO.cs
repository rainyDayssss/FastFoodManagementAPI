namespace FastFoodManagementAPI.Features.Orders
{
    public class CreateOrderItemDTO
    {
        // Product being ordered
        public int ProductId { get; set; }

        // Quantity of this product
        public int Quantity { get; set; }

    }
}
