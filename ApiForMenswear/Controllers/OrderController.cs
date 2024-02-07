using ApiForMenswear.Interface;
using ApiForMenswear.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApiForMenswear.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;

        public OrderController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        [HttpGet("Order")]
        public ActionResult<IEnumerable<Order>> GetAllOrders()
        {
            var order = _orderRepository.GetAllOrders();
            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        [HttpGet("Order/{id}")]
        public ActionResult<Order> GetOrderById(int orderId)
        {
            var order = _orderRepository.GetOrderById(orderId);
            if (order == null)
            {
                return NotFound();
            }
            return order;
        }

        [HttpPost("Order")]
        public ActionResult<Order> AddOrder(Order order)
        {
            if (order == null)
            {
                return BadRequest("Order is Null");
            }

            _orderRepository.AddOrder(order);
            return Ok("Order Successfully Added");
        }

        [HttpPut("Order/{id}")]
        public ActionResult UpdateOrder(Order order, int orderId)
        {
            try
            {
                _orderRepository.UpdateOrder(order, orderId);
            }
            catch (ArgumentException)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("Order/{id}")]
        public ActionResult DeleteOrder(int orderId)
        {
            try
            {
                _orderRepository.DeleteOrder(orderId);
            }
            catch (ArgumentException)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
