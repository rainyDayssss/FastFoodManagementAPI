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
            _context.Products.Remove(product);
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
    }
}
