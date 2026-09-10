using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            var orders = await _orderService.GetAllOrdersAsync();
            return Ok(orders);
        }

        [HttpGet]
        public async Task<IActionResult> GetOrderByCustomerId(int id)
        {
            var order = await _orderService.GetOrdersByCustomerIdAsync(id);
            if (order == null || !order.Any())
            {
                return NotFound("No Orders found for the specified Customer ID {id}.");
            }
            return Ok(order);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] Order order)
        {
            if (order == null)
            {
                return BadRequest("Order data is null.");
            }
            await _orderService.SaveOrderAsync(order);
            return CreatedAtAction(nameof(GetAllOrders), new { id = order.Id }, order);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateOrder(int id, [FromBody] Order order)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid Order ID.");
            }
            if(order.Id != id)
            {
                return BadRequest("Order ID mismatch.");
            }
            if (order == null)
            {
                return BadRequest("Order data is null.");
            }

            await _orderService.UpdateOrderAsync(order);
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var existingOrder = await _orderService.GetOrderByIdAsync(id);
            if (existingOrder == null)
            {
                return NotFound("Order not found.");
            }
            await _orderService.DeleteOrderAsync(id);
            return NoContent();
        }
    }
}
