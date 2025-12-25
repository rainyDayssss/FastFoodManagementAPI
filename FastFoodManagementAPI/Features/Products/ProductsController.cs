using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;

namespace FastFoodManagementAPI.Features.Products;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly ProductService _productService;

    public ProductsController(ProductService productService)
    {
        _productService = productService;
    }

    // Create product
    [HttpPost]
    public async Task<IActionResult> AddProduct([FromForm] CreateProductDTO dto, IFormFile Image)
    {
        var product = await _productService.AddProductAsync(dto, Image);
        // Build full URL
        var baseUrl = $"{Request.Scheme}://{Request.Host}";
        product.ImagePath = $"{baseUrl}/{product.ImagePath.Replace("\\", "/")}";
        return Ok(product);
    }

    // Get all products
    [HttpGet]
    public async Task<IActionResult> GetAllProducts()
    {
        var products = await _productService.GetAllProductsAsync();
        var baseUrl = $"{Request.Scheme}://{Request.Host}";

        // Prepend base URL for each product
        foreach (var product in products)
        {
            if (!string.IsNullOrEmpty(product.ImagePath))
                product.ImagePath = $"{baseUrl}/{product.ImagePath.Replace("\\", "/")}";
        }

        return Ok(products);
    }

    // Get product
    [HttpGet("{productId}")]
    public async Task<IActionResult> GetProduct(int productId)
    {
        var product = await _productService.GetProductAsync(productId);
        var baseUrl = $"{Request.Scheme}://{Request.Host}";

        if (!string.IsNullOrEmpty(product.ImagePath))
            product.ImagePath = $"{baseUrl}/{product.ImagePath.Replace("\\", "/")}";

        return Ok(product);
    }

    // Patch product
    [HttpPatch("{productId}")]
    public async Task<IActionResult> UpdateProduct(int productId, [FromForm] PatchProductDTO patchProductDTO, IFormFile? Image)
    {
        var product = await _productService.UpdateProductAsync(productId, patchProductDTO, Image);
        var baseUrl = $"{Request.Scheme}://{Request.Host}";

        if (!string.IsNullOrEmpty(product.ImagePath))
            product.ImagePath = $"{baseUrl}/{product.ImagePath.Replace("\\", "/")}";

        return Ok(product);
    }

    // Delete product
    [HttpDelete("{productId}")]
    public async Task<IActionResult> DeleteProduct(int productId) 
    {
        return Ok(await _productService.DeleteProductAsync(productId));
    }
}
