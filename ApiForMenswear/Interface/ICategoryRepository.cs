using ApiForMenswear.Models;

namespace ApiForMenswear.Interface
{
	public interface ICategoryRepository
	{
		IEnumerable<Category> GetAllCategory();
		Category GetCategoryById(int id);
		void AddCategory(Category category);
		void UpdateCategory(Category category, int id);
		void DeleteCategory(int id);
	}
}
