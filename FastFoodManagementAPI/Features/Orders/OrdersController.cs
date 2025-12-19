using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FastFoodManagementAPI.Features.Orders
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly OrderService _orderService;
        public OrdersController(OrderService orderService)
        {
            _orderService = orderService;
        }

        // Get all orders
        [HttpGet]
        public async Task<IActionResult> GetAllOrdersAsync() 
        {
            return Ok(await _orderService.GetAllOrdersAsync());
        }

        // Get an order
        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetOrderByIdAsync(int orderId)
        {
            return Ok(await _orderService.GetOrderByIdAsync(orderId));
        }

        // Create order
        [HttpPost]
        public async Task<IActionResult> CreateOrderAsync(CreateOrderDTO createOrderDTO) 
        {
            return Ok(await _orderService.CreateOrderAsync(createOrderDTO));
        }

        // Change order status
        [HttpPatch("{orderId}")]
        public async Task<IActionResult> ChangeOrderStatusAsync(int orderId, [FromBody] PatchOrderDTO patchOrderDTO) 
        {
            return Ok(await _orderService.ChangeOrderStatusAsync(orderId, patchOrderDTO));
        }


        // Get all orders by orderstatus 
        [HttpGet("status/{status}")] // Confirmed, Completed, Paid
        public async Task<IActionResult> GetAllOrdersByStatusAsync(OrderStatus status)
        {
           return Ok(await _orderService.GetAllOrdersByStatusAsync(status));
        }
    }
}
