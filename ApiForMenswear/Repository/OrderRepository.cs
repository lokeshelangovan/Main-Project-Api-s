using ApiForMenswear.Data;
using ApiForMenswear.Interface;
using ApiForMenswear.Models;

namespace ApiForMenswear.Repository
{
	public class OrderRepository : IOrderRepository
	{
		private readonly MenswearContext _context;

		public OrderRepository(MenswearContext context)
		{
			_context = context;
		}
		public void AddOrder(Order order)
		{
			_context.Order.Add(order);
			_context.SaveChanges();
		}

		public void DeleteOrder(int orderId)
		{
			var orderToRemove = _context.Order.FirstOrDefault(o => o.Id == orderId);
			if (orderToRemove != null)
			{
				_context.Order.Remove(orderToRemove);
				_context.SaveChanges();

			}
			else
			{
				throw new ArgumentException("Order not found");
			}
		}

		public IEnumerable<Order> GetAllOrders()
		{
			return _context.Order.ToList();
		}

		public Order GetOrderById(int orderId)
		{
			return _context.Order.FirstOrDefault(o => o.Id == orderId);
		}

		public void UpdateOrder(Order order, int orderId)
		{
			var existingOrder = _context.Order.FirstOrDefault(o => o.Id == orderId);
			if (existingOrder != null)
			{
				existingOrder.UserSignupId = order.UserSignupId;				
				existingOrder.OrderDate = order.OrderDate;								
				existingOrder.ShippingAddress = order.ShippingAddress ?? existingOrder.ShippingAddress;
				existingOrder.TotalAmount = order.TotalAmount;
				existingOrder.OrderStatus = order.OrderStatus ?? existingOrder.OrderStatus;
				existingOrder.FullName = order.FullName ?? existingOrder.FullName;
				existingOrder.CreditCard = order.CreditCard;

				_context.SaveChanges();
			}
			else
			{
				throw new ArgumentException("Order not found");
			}
		}
	}
}
