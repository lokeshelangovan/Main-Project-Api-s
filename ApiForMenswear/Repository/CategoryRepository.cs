using ApiForMenswear.Data;
using ApiForMenswear.Interface;
using ApiForMenswear.Models;

namespace ApiForMenswear.Repository
{
	public class CategoryRepository : ICategoryRepository
	{
		private readonly ApiContext _context;

		public CategoryRepository(ApiContext context)
		{
			_context = context;
		}
		public void AddCategory(Category category)
		{
			_context.Category.Add(category);
			_context.SaveChanges();
		}

		public void DeleteCategory(int id)
		{
			var orderToRemove = _context.Category.FirstOrDefault(o => o.Id == id);
			if (orderToRemove != null)
			{
				_context.Category.Remove(orderToRemove);
				_context.SaveChanges();

			}
			else
			{
				throw new ArgumentException("Category not found");
			}
		}

		public IEnumerable<Category> GetAllCategory()
		{
			return _context.Category.ToList();
		}

		public Category GetCategoryById(int id)
		{
			return _context.Category.FirstOrDefault(o => o.Id == id);
		}

		public void UpdateCategory(Category category, int id)
		{
			var existingOrder = _context.Category.FirstOrDefault(o => o.Id == id);
			if (existingOrder != null)
			{

				existingOrder.Name = category.Name ?? category.Name;

				_context.SaveChanges();
			}
			else
			{
				throw new ArgumentException("Order not found");
			}
		}
	}
}
