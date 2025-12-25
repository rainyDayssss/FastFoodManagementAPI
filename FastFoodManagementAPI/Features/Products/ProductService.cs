namespace FastFoodManagementAPI.Features.Products;

public class ProductService
{
    private readonly ProductRepository _productRepository;
    private readonly IWebHostEnvironment _environment;

    public ProductService(ProductRepository productRepository, IWebHostEnvironment environment)
    {
        _productRepository = productRepository;
        _environment = environment;
    }


    // Create product
    public async Task<Product> AddProductAsync(CreateProductDTO dto, IFormFile image)
    {
        if (image == null || image.Length == 0)
            throw new ArgumentException("Product image is required");

        // Check if an inactive product with the same name exists
        var existingProduct = await _productRepository
            .GetProductByNameAsync(dto.Name);

        if (existingProduct != null)
        {
            // Reactivate product
            existingProduct.Stock += dto.Stock; // add new stock
            existingProduct.Price = dto.Price;   // update price if needed
            existingProduct.IsActive = true;

            // Update image if a new one is provided
            var folderPath = Path.Combine(_environment.WebRootPath, "images/products");
            Directory.CreateDirectory(folderPath);

            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(image.FileName)}";
            var filePath = Path.Combine(folderPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }

            existingProduct.ImagePath = $"images/products/{fileName}";

            return await _productRepository.UpdateProductAsync(existingProduct);
        }

        // Save new image
        var folder = Path.Combine(_environment.WebRootPath, "images/products");
        Directory.CreateDirectory(folder);

        var newFileName = $"{Guid.NewGuid()}{Path.GetExtension(image.FileName)}";
        var newFilePath = Path.Combine(folder, newFileName);

        using (var stream = new FileStream(newFilePath, FileMode.Create))
        {
            await image.CopyToAsync(stream);
        }

        // Create new product entity
        var product = new Product
        {
            Name = dto.Name,
            Price = dto.Price,
            Stock = dto.Stock,
            IsActive = true,
            ImagePath = $"images/products/{newFileName}"
        };

        return await _productRepository.AddProductAsync(product);
    }

    // Get all products
    public async Task<List<Product>> GetAllProductsAsync()
    {
        return await _productRepository.GetAllProductsAsync();
    }

    // Get product
    public async Task<Product> GetProductAsync(int productId)
    {
        var product = await _productRepository.GetProductByIdAsync(productId);
        if (product is null)
        {
            throw new Exception("Product not found");
        }
        return product;
    }

    // Update product
    public async Task<Product> UpdateProductAsync(int productId, PatchProductDTO patchProductDTO, IFormFile? image)
    {
        var product = await _productRepository.GetProductByIdAsync(productId);
        if (product == null)
        {
            throw new Exception("Product not found");
        }

        // Delete old image if new one uploaded
        if (image != null && !string.IsNullOrEmpty(product.ImagePath))
        {
            var oldPath = Path.Combine(_environment.WebRootPath, product.ImagePath);
            if (File.Exists(oldPath))
                File.Delete(oldPath);
        }

        // Update fields
        if (!string.IsNullOrEmpty(patchProductDTO.Name)) product.Name = patchProductDTO.Name;
        if (patchProductDTO.Price.HasValue) product.Price = patchProductDTO.Price.Value;
        if (patchProductDTO.Stock.HasValue) product.Stock = patchProductDTO.Stock.Value;

        // Handle optional new image
        if (image != null && image.Length > 0)
        {
            var folderPath = Path.Combine(_environment.WebRootPath, "images/products");
            Directory.CreateDirectory(folderPath);

            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(image.FileName)}";
            var filePath = Path.Combine(folderPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }
            
            product.ImagePath = $"images/products/{fileName}";
        }

        return await _productRepository.UpdateProductAsync(product);
    }

    // Delete product
    public async Task<Product> DeleteProductAsync(int productId)
    {
        var product = await _productRepository.GetProductByIdAsync(productId);
        if (product is null)
        {
            throw new Exception("Product not found");
        }

        // Optional: delete image file as well
        if (!string.IsNullOrEmpty(product.ImagePath))
        {
            var filePath = Path.Combine(_environment.WebRootPath, product.ImagePath);
            if (File.Exists(filePath))
                File.Delete(filePath);
        }

        await _productRepository.DeleteProductAsync(product);
        return product;

    }
}
