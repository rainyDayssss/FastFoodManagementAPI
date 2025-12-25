using FastFoodManagementAPI.Shared;
using Microsoft.EntityFrameworkCore;

namespace FastFoodManagementAPI.Features.Orders;

public class OrderRepository
{
    private readonly FastFoodDbContext _context;
    public OrderRepository(FastFoodDbContext context) 
    {   
        _context = context;
    }

    // ORDERS
    // Add order
    public async Task<Order> AddOrderAsync(Order order) 
    {
        await _context.Orders.AddAsync(order);
        await _context.SaveChangesAsync();
        return order;
    }

    // Get all orders
    public async Task<List<Order>> GetAllOrdersAsync() 
    {
        return await _context.Orders
           .Include(o => o.OrderItems)
           .ThenInclude(oi => oi.Product)
           .ToListAsync();
    }
    
    // Get an order
    public async Task<Order?> GetOrderByIdAsync(int orderId)
    {
        return await _context.Orders
            .Include(o => o.OrderItems)
            .ThenInclude(oi => oi.Product)
            .FirstOrDefaultAsync(o => o.Id == orderId);
    }

    // Patch update order
    public async Task<Order?> UpdateOrderAsync(Order order) 
    {
        _context.Orders.Update(order);
        await _context.SaveChangesAsync();
        return order;
    }

    // Get all orders by OrderStatus
    public async Task<List<Order>> GetOrdersByStatusAsync(OrderStatus status)
    {
        return await _context.Orders
            .Where(o => o.OrderStatus == status)
            .Include(o => o.OrderItems)
            .ThenInclude(oi => oi.Product)
            .ToListAsync();
    }

}
