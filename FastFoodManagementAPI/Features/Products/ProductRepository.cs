using FastFoodManagementAPI.Features.Orders;
using FastFoodManagementAPI.Shared;
using Microsoft.EntityFrameworkCore;

namespace FastFoodManagementAPI.Features.Products
{
    public class ProductRepository
    {
        private readonly FastFoodDbContext _context;

        public ProductRepository(FastFoodDbContext context)
        {
            _context = context;
        }

        // Create
        public async Task<Product> AddProductAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return product;
        }

        // Get all
        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await _context.Products.ToListAsync();
        }

        // Get by id
        public async Task<Product?> GetProductByIdAsync(int productId)
        {
            return await _context.Products.FindAsync(productId);
        }

        // Update (dedicated method)
        public async Task<Product> UpdateProductAsync(Product product)
        {
            _context.Products.Update(product); // Marks the entity as modified
            await _context.SaveChangesAsync();
            return product;
        }

        // Delete
        public async Task<Product?> DeleteProductAsync(Product product)
        {
            if (product == null) return null;

            product.IsActive = false;  // mark as inactive
            product.Stock = 0;         // optionally set stock to 0

            await _context.SaveChangesAsync();
            return product;
        }

        // Get products by ids
        public async Task<List<Product>> GetProductsByIdsAsync(List<int> ids)
        {
            return await _context.Products
                .Where(p => ids.Contains(p.Id))
                .ToListAsync();
        }

        // Deduct product quantity
        public async Task DeductStockByProductsAsync(List<OrderItem> orderItems) 
        {
            var productIds = orderItems.Select(i => i.ProductId).ToList();

            var products = await _context.Products
                .Where(p => productIds.Contains(p.Id))
                .ToListAsync();

            var productDict = products.ToDictionary(p => p.Id);

            foreach (var item in orderItems)
            {
                var product = productDict[item.ProductId];
                product.Stock -= item.Quantity;

                if (product.Stock <= 0)
                {
                    product.Stock = 0;
                    product.IsActive = false;
                }
            }

            await _context.SaveChangesAsync();
        }

        // Get product by name
        public async Task<Product?> GetProductByNameAsync(string name)
        {
            // Include inactive products so we can reactivate them if needed
            return await _context.Products
                .FirstOrDefaultAsync(p => p.Name.ToLower() == name.ToLower());
        }
    }
}
