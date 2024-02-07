using ApiForMenswear.Models;

namespace ApiForMenswear.Interface
{
    public interface IOrderItemRepository
    {
        IEnumerable<OrderItem> GetOrderItemsByOrderId(int orderId);
        int AddOrderItem(OrderItem orderItem);
        OrderItem GetOrderItemById(int orderItemId);
        bool UpdateOrderItem(OrderItem updatedOrderItem);
        bool DeleteOrderItem(int orderItemId);
    }
}
