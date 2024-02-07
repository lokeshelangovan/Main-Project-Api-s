using ApiForMenswear.Interface;
using ApiForMenswear.Models;
using ApiForMenswear.Repository;
using Microsoft.AspNetCore.Mvc;

namespace ApiForMenswear.Controllers
{
    public class OrderItemController : Controller
    {
        private readonly IOrderItemRepository _orderItemRepository;

        public OrderItemController(IOrderItemRepository orderItemRepository)
        {
            _orderItemRepository = orderItemRepository;
        }

        [HttpGet("OrderItems/{orderId}")]
        public IActionResult GetOrderItemsByOrderId(int orderId)
        {
            var orderItems = _orderItemRepository.GetOrderItemsByOrderId(orderId);
            return Ok(orderItems);
        }

        [HttpPost("OrderItem")]
        public IActionResult AddOrderItem( OrderItem orderItem)
        {
            if (orderItem == null)
            {
                return BadRequest("Invalid order item data");
            }

            _orderItemRepository.AddOrderItem(orderItem);
            return Ok("Order Successfully Added");
        }

        [HttpGet("OrderItem/{orderItemId}")]
        public IActionResult GetOrderItemById(int orderItemId)
        {
            var orderItem = _orderItemRepository.GetOrderItemById(orderItemId);

            if (orderItem == null)
            {
                return NotFound();
            }

            return Ok(orderItem);
        }

        [HttpPut("OrderItem/{orderItemId}")]
        public IActionResult UpdateOrderItem(int orderItemId, OrderItem updatedOrderItem)
        {
            if (updatedOrderItem == null || orderItemId != updatedOrderItem.Id)
            {
                return BadRequest("Invalid request");
            }

            var success = _orderItemRepository.UpdateOrderItem(updatedOrderItem);

            if (success)
            {
                return NoContent();
            }

            return NotFound();
        }

        [HttpDelete("OrderItem/{orderItemId}")]
        public IActionResult DeleteOrderItem(int orderItemId)
        {
            var success = _orderItemRepository.DeleteOrderItem(orderItemId);

            if (success)
            {
                return NoContent();
            }

            return NotFound();
        }
    }
}
