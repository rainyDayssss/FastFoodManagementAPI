using FastFoodManagementAPI.Shared;

namespace FastFoodManagementAPI.Features.Orders
{
    public class OrderRepository
    {
        private readonly FastFoodDbContext _context;
        public OrderRepository(FastFoodDbContext context) 
        {
            _context = context;
        }

        // Add order
        // Get all orders
        // Get an order
    }
}
