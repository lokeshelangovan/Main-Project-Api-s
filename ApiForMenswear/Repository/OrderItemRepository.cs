using ApiForMenswear.Data;
using ApiForMenswear.Interface;
using ApiForMenswear.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiForMenswear.Repository
{
    public class OrderItemRepository : IOrderItemRepository
    {
        private readonly MenswearContext _context;

        public OrderItemRepository(MenswearContext context)
        {
            _context = context;
        }
        public int AddOrderItem(OrderItem orderItem)
        {
            _context.OrderItem.Add(orderItem);
            _context.SaveChanges();
            return orderItem.Id;
        }

        public bool DeleteOrderItem(int orderItemId)
        {
            var orderItem = _context.OrderItem.Find(orderItemId);

            if (orderItem != null)
            {
                _context.OrderItem.Remove(orderItem);
                _context.SaveChanges();
                return true;
            }

            return false;
        }

        public OrderItem GetOrderItemById(int orderItemId)
        {
            return _context.OrderItem.FirstOrDefault(oi => oi.Id == orderItemId);
        }

        public IEnumerable<OrderItem> GetOrderItemsByOrderId(int orderId)
        {
            return _context.OrderItem
       .Where(oi => oi.OrderId == orderId)       
       .ToList();
        }

        public bool UpdateOrderItem(OrderItem updatedOrderItem)
        {
            var existingOrderItem = _context.OrderItem.Find(updatedOrderItem.Id);

            if (existingOrderItem != null)
            {
                existingOrderItem.OrderId = updatedOrderItem.OrderId;
                existingOrderItem.ProductId = updatedOrderItem.ProductId;
                existingOrderItem.Quantity = updatedOrderItem.Quantity;
                existingOrderItem.Price = updatedOrderItem.Price;
                // Update other properties as needed

                _context.SaveChanges();
                return true;
            }

            return false;
        }
        
    }
}
