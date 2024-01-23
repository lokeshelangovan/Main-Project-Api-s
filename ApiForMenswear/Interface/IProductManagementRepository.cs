using ApiForMenswear.Models;

namespace ApiForMenswear.Interface
{
	public interface IProductManagementRepository
	{
		IEnumerable<ProductManagement> GetAllProducts();
		ProductManagement GetProductById(int id);
		ProductManagement GetByProductName(string name);
		void AddProduct(ProductManagement product);
		void UpdateProduct(ProductManagement product, int id);
		void DeleteProduct(int id);
	}
}
