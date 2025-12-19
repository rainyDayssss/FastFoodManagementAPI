using FastFoodManagementAPI.Features.Products;

namespace FastFoodManagementAPI.Features.Orders
{
    public class OrderService
    {
        private readonly OrderRepository _orderRepository;
        private readonly ProductRepository _productRepository;
        public OrderService(OrderRepository orderRepository, ProductRepository productRepository)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
        }
        // Create order
        public async Task<Order> CreateOrderAsync(CreateOrderDTO createOrderDTO)
        {
            // check if the products existed
            var productIdsNeeded = createOrderDTO.Items.Select(i => i.ProductId).ToList();

            // existing products
            var products = await _productRepository.GetProductsByIdsAsync(productIdsNeeded);

            // IDs of existing products
            var existingProductIds = products.Select(p => p.Id).ToList();

            // Find missing products
            var missingProducts = productIdsNeeded.Except(existingProductIds).ToList();

            if (missingProducts.Any())
            {
                throw new Exception($"Products not found: {string.Join(", ", missingProducts)}");
            }


            // dto to entity (map OrderItems with real prices)
            var order = new Order
            {
                TableId = createOrderDTO.TableId,
                OrderStatus = OrderStatus.Confirmed,
                OrderItems = createOrderDTO.Items.Select(dto =>
                {
                    // find the product in DB
                    var product = products.First(p => p.Id == dto.ProductId);

                    return new OrderItem
                    {
                        ProductId = dto.ProductId,
                        Quantity = dto.Quantity,
                        UnitPrice = product.Price // fetch price from DB
                    };
                }).ToList(),
                Payment = null // to be paid later
            };
            
            // save order
            return await _orderRepository.AddOrderAsync(order);
        }

        // Get all orders

        public async Task<List<Order>> GetAllOrdersAsync()
        {
            return await _orderRepository.GetAllOrdersAsync();
        }

        // Get an order
        public async Task<Order> GetOrderByIdAsync(int orderId)
        {
            var order = await _orderRepository.GetOrderByIdAsync(orderId);
            if (order is null) 
            {
                throw new Exception("Order was not found");
            }
            return order;
        }

        // Change order status
        public async Task<Order> ChangeOrderStatusAsync(int orderId, PatchOrderDTO patchOrderDTO) 
        {
            var order = await _orderRepository.GetOrderByIdAsync(orderId);
            if (order is null)
            {
                throw new Exception("Order was not found");
            }

            if(patchOrderDTO.OrderStatus is not null)
                order.OrderStatus = patchOrderDTO.OrderStatus.Value;

            await _orderRepository.UpdateOrderAsync(order);
            return order;
        }

        // Get all orders by order status
        public async Task<List<Order>> GetAllOrdersByStatusAsync(OrderStatus orderStatus) 
        {
            return await _orderRepository.GetOrdersByStatusAsync(orderStatus);
        }
    }
}
