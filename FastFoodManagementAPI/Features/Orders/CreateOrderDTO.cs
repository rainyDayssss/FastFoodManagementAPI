namespace FastFoodManagementAPI.Features.Orders;

public class CreateOrderDTO
{
    // Optional table for dine-in, null for takeout
    public int? TableId { get; set; }

    // List of products being ordered with quantity
    public List<CreateOrderItemDTO> Items { get; set; } = new();
}
