using ApiForMenswear.Interface;
using ApiForMenswear.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace ApiForMenswear.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductManagementController : ControllerBase
    {
        private readonly IProductManagementRepository _productManagementRepository;

        public ProductManagementController(IProductManagementRepository productManagementRepository)
        {
            _productManagementRepository = productManagementRepository;
        }

        [HttpGet("Product")]
        public ActionResult<IEnumerable<ProductManagement>> GetAllProducts()
        {
            var products = _productManagementRepository.GetAllProducts();
            if (products == null)
            {
                return NotFound();
            }
           

            return Ok(products);
        }



        [HttpGet("Product/{id}")]
        public ActionResult<ProductManagement> GetProductById(int id)
        {
            var product = _productManagementRepository.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            
            return product;
        }

       

        [HttpGet("Product/byName/{name}")]
        public ActionResult<ProductManagement> GetByProductName(string name)
        {
            var product = _productManagementRepository.GetByProductName(name);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }


        [HttpPost("Product")]
        public ActionResult<ProductManagement> AddProduct( ProductManagement product)
        {
           

            try
            {
                if (product == null || string.IsNullOrEmpty(product.Name))
                {
                    return BadRequest("Product Data is Null");
                }
                Console.WriteLine($"Received product: {JsonConvert.SerializeObject(product)}");

                _productManagementRepository.AddProduct(product);
                Console.WriteLine("Product Successfully Added");

                return Ok("Product Successfully Added");
            }
            catch (DbUpdateException ex)
            {
               
                Console.WriteLine($"DbUpdateException: {ex.InnerException?.Message}");
                return StatusCode(500, "Error updating the database. Please check the logs.");
            }
            catch (Exception ex)
            {
                
               
                Console.WriteLine($"StackTrace: {ex.StackTrace}");
                Console.WriteLine($"Error adding product: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }


        [HttpPut("Product/{id}")]
        public ActionResult UpdateProduct(ProductManagement product, int id)
        {
            try
            {
                _productManagementRepository.UpdateProduct(product, id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                Console.WriteLine($"StackTrace: {ex.StackTrace}");
                return NotFound();
            }
            return NoContent();
        }


        [HttpDelete("Product/{id}")]
        public ActionResult DeleteProduct(int id)
        {
            try
            {
                _productManagementRepository.DeleteProduct(id);
            }
            catch (ArgumentException)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
