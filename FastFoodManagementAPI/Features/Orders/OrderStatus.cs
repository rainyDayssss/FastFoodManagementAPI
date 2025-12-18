namespace FastFoodManagementAPI.Features.Orders
{
    public enum OrderStatus
    {
        Confirmed, // sent to kitchen
        Completed, // sent to cashier
        Paid
    }
}
