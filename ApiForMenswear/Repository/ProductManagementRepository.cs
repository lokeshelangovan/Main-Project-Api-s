using ApiForMenswear.Data;
using ApiForMenswear.Interface;
using ApiForMenswear.Models;

namespace ApiForMenswear.Repository
{
	public class ProductManagementRepository : IProductManagementRepository
	{
		private readonly ApiContext _context;

		public ProductManagementRepository(ApiContext context)
		{
			_context = context;
		}
		public void AddProduct(ProductManagement product)

		{
			_context.ProductManagement.Add(product);
			_context.SaveChanges();
		}

		public void DeleteProduct(int id)
		{
			var productToRemove = _context.ProductManagement.FirstOrDefault(o => o.Id == id);
			if (productToRemove != null)
			{
				_context.ProductManagement.Remove(productToRemove);
				_context.SaveChanges();

			}
			else
			{
				throw new ArgumentException("Product not found");
			}
		}

		public IEnumerable<ProductManagement> GetAllProducts()
		{
			var product = _context.ProductManagement.ToList();
			return product;
		}

		public ProductManagement GetByProductName(string name)
		{
			return _context.ProductManagement.FirstOrDefault(p => p.Name.ToLower() == name.ToLower());
		}

		public ProductManagement GetProductById(int id)
		{
			return _context.ProductManagement.FirstOrDefault(s => s.Id == id);
		}

		public void UpdateProduct(ProductManagement product, int id)
		{
			var existingProduct = _context.ProductManagement.FirstOrDefault(s => s.Id == id);
			if (existingProduct != null)
			{
				existingProduct.Name = product.Name ?? existingProduct.Name;
				existingProduct.Description = product.Description ?? existingProduct.Description;
				existingProduct.Price = product.Price ?? existingProduct.Price;
				existingProduct.Category = product.Category;
				existingProduct.ImageUrl = product.ImageUrl ?? existingProduct.ImageUrl;
				existingProduct.Quantity = product.Quantity;

				_context.SaveChanges();
			}
			else
			{
				throw new ArgumentException("Product not found");
			}
		}
	}
}
