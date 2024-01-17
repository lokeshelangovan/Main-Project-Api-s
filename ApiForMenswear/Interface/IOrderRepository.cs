using ApiForMenswear.Models;

namespace ApiForMenswear.Interface
{
	public interface IOrderRepository
	{
		IEnumerable<Order> GetAllOrders();
		Order GetOrderById(int orderId);
		void AddOrder(Order order);
		void UpdateOrder(Order order, int orderId);
		void DeleteOrder(int orderId);
	}
}
