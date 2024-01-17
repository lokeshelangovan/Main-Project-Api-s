using ApiForMenswear.Interface;
using ApiForMenswear.Models;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using Newtonsoft.Json;
using ApiForMenswear.Utilities;


namespace ApiForMenswear.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class MenswearController : ControllerBase
	{
		private readonly IUserSignupRepository _userSignupRepository;
		private readonly ISellerSignupRepository _sellerSignupRepository;
		private readonly IProductManagementRepository _productManagementRepository;
		private readonly IOrderRepository _orderRepository;
		private readonly ICategoryRepository _categoryRepository;

		public MenswearController(IUserSignupRepository userSignupRepository, ISellerSignupRepository sellerSignupRepository,
			IProductManagementRepository productManagementRepository, IOrderRepository orderRepository, ICategoryRepository categoryRepository)
		{
			_userSignupRepository = userSignupRepository;
			_sellerSignupRepository = sellerSignupRepository;
			_productManagementRepository = productManagementRepository;
			_orderRepository = orderRepository;
			_categoryRepository = categoryRepository;
		}
		//------------------------------------Password Hashing-------------------------------------
		//private string HashPassword(string password)
		//{
		//	using SHA256 sha256 = SHA256.Create();

		//	// Compute hash from password
		//	byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

		//	// Convert byte array to a hexadecimal string
		//	string hashedPassword = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();

		//	return hashedPassword;
		//}
		//-----------------------------SellerSignup API-------------------------

		[HttpGet("Signup")]
		public ActionResult<IEnumerable<SellerSignup>> GetAll()
		{
			var signup = _sellerSignupRepository.GetAll();
			if (signup == null)
			{
				return NotFound();
			}

			return Ok(signup);
		}

		[HttpGet("Signup/{id}")]
		public ActionResult<SellerSignup> GetSignupById(int id)
		{
			var signup = _sellerSignupRepository.GetById(id);
			if (signup == null)
			{
				return NotFound();
			}
			return signup;
		}

		[HttpPost("Signup")]
		public ActionResult<SellerSignup> Insert(SellerSignup signup)
		{
			if (signup == null)
			{
				return BadRequest("Signup Data is Null");
			}
			

			_sellerSignupRepository.Insert(signup);
			return Ok(new { Message = "Signup Successfully Added" });
		}

		[HttpPut("Signup/{id}")]
		public ActionResult UpdateSignup(int id, SellerSignup signup)
		{
			try
			{
				// Retrieve the existing user from the database
				var existingSignup = _sellerSignupRepository.GetById(id);

				if (existingSignup != null)
				{
					// Check if the password is provided in the request
					if (!string.IsNullOrEmpty(signup.Password))
					{
						// Hash the new password
						string hashedPassword = PasswordUtilities.HashPassword(signup.Password);

						// Update the existing user's password
						existingSignup.Password = hashedPassword;
					}

					// Update other properties if needed
					existingSignup.Name = signup.Name ?? existingSignup.Name;
					existingSignup.Email = signup.Email ?? existingSignup.Email;

					// Save changes to the database
					_sellerSignupRepository.UpdateSignup(id, existingSignup);

					return NoContent();
				}
				else
				{
					return NotFound();
				}
			}
			catch (ArgumentException)
			{
				return NotFound();
			}
		}

		[HttpDelete("Signup/{id}")]
		public ActionResult DeleteSignup(int id)
		{
			try
			{
				_sellerSignupRepository.DeleteSignup(id);
			}
			catch (ArgumentException)
			{
				return NotFound();
			}
			return NoContent();
		}

		[HttpPost("SellerLogin")]
		public ActionResult<SellerSignup> SellerLogin(SellerLoginRequest loginRequest)
		{
			var user = _sellerSignupRepository.GetByEmail(loginRequest.Email);

			if (user != null && PasswordUtilities.VerifyPassword(loginRequest.Password, user.Password))
			{
				// Login successful
				// Return user details or a token, depending on your authentication mechanism
				return Ok(new { Message = "Login Successful" });
			}
			else
			{
				// Login failed
				return Unauthorized("Invalid email or password.");
			}
		}
		//--------------------------------------END--------------------------------------------
		//----------------------------ProductManagement API-----------------------------------
		[HttpGet("Product")]
		public ActionResult<IEnumerable<ProductManagement>> GetAllProducts()
		{
			var product = _productManagementRepository.GetAllProducts();
			if (product == null)
			{
				return NotFound();
			}

			return Ok(product);
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
		public ActionResult<ProductManagement> AddProduct([FromBody] ProductManagement product)
		{
            Console.WriteLine($"Received data: {JsonConvert.SerializeObject(product)}");

            try
            {
                if (product == null)
                {
                    return BadRequest("Product Data is Null");
                }

                _productManagementRepository.AddProduct(product);

                return Ok("Product Successfully Added");
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
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
			catch (ArgumentException)
			{
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
		//-------------------------End-----------------------------------------------------
		//---------------------------UserSignup---------------------------------------------
		[HttpGet("UserSignup")]
		public ActionResult<IEnumerable<UserSignup>> GetAllUserDetail()
		{
			var signup = _userSignupRepository.GetAll();
			if (signup == null)
			{
				return NotFound();
			}

			return Ok(signup);
		}

		[HttpGet("UserSignup/{id}")]
		public ActionResult<UserSignup> GetUserSignupById(int id)
		{
			var signup = _userSignupRepository.GetById(id);
			if (signup == null)
			{
				return NotFound();
			}
			return signup;
		}

		[HttpPost("UserSignup")]
		public ActionResult<UserSignup> AddUser(UserSignup signup)
		{
			if (signup == null)
			{
				return BadRequest("Signup Data is Null");
			}

			

			_userSignupRepository.Insert(signup);
			return Ok("Signup Successfully Added");
		}

		[HttpPut("UserSignup/{id}")]
		public ActionResult UpdateUserSignup(int id, UserSignup signup)
		{
			try
			{
				// Retrieve the existing user from the database
				var existingSignup = _userSignupRepository.GetById(id);

				if (existingSignup != null)
				{
					// Check if the password is provided in the request
					if (!string.IsNullOrEmpty(signup.Password))
					{
						// Hash the new password
						string hashedPassword = PasswordUtilities.HashPassword(signup.Password);

						// Update the existing user's password
						existingSignup.Password = hashedPassword;
					}

					// Update other properties if needed
					existingSignup.Username = signup.Username ?? existingSignup.Username;
					existingSignup.Email = signup.Email ?? existingSignup.Email;

					// Save changes to the database
					_userSignupRepository.UpdateSignup(id, existingSignup);

					return NoContent();
				}
				else
				{
					return NotFound();
				}
			}
			catch (ArgumentException)
			{
				return NotFound();
			}
		}

		[HttpDelete("UserSignup/{id}")]
		public ActionResult DeleteUserSignup(int id)
		{
			try
			{
				_userSignupRepository.DeleteSignup(id);
			}
			catch (ArgumentException)
			{
				return NotFound();
			}
			return NoContent();
		}

		[HttpDelete("UserSignup")]
		public ActionResult DeleteAllUsers()
		{
			_userSignupRepository.DeleteAllUsers();
			return Ok("All users deleted successfully");

		}
		[HttpPost("UserLogin")]
		public ActionResult<UserSignup> UserLogin(UserLoginRequest loginRequest)
		{
			var user = _userSignupRepository.GetByEmail(loginRequest.Email);

			if (user != null && PasswordUtilities.VerifyPassword(loginRequest.Password, user.Password))
			{
				// Login successful
				// Return user details or a token, depending on your authentication mechanism
				return Ok(new { Message = "Login Successful" });
			}
			else
			{
				// Login failed
				return Unauthorized("Invalid email or password.");
			}
		}

		//----------------------------------------UserSignup End------------------------------------
		//----------------------------------------OrdeR---------------------------------------------
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
		//---------------------------------Category--------------------------------------------------------------
		[HttpGet("Category")]
		public ActionResult<IEnumerable<Category>> GetAllCategory()
		{
			var category = _categoryRepository.GetAllCategory();
			if (category == null)
			{
				return NotFound();
			}

			return Ok(category);
		}

		[HttpGet("Category/{id}")]
		public ActionResult<Category> GetCategoryById(int id)
		{
			var category = _categoryRepository.GetCategoryById(id);
			if (category == null)
			{
				return NotFound();
			}
			return category;
		}

		[HttpPost("Category")]
		public ActionResult<Category> AddCategory(Category category)
		{
			if (category == null)
			{
				return BadRequest("Category is Null");
			}

			_categoryRepository.AddCategory(category);
			return Ok("Category Successfully Added");
		}

		[HttpPut("Category/{id}")]
		public ActionResult UpdateCategory(Category category, int id)
		{
			try
			{
				_categoryRepository.UpdateCategory(category, id);
			}
			catch (ArgumentException)
			{
				return NotFound();
			}
			return NoContent();
		}

		[HttpDelete("Category/{id}")]
		public ActionResult DeleteCategory(int id)
		{
			try
			{
				_categoryRepository.DeleteCategory(id);
			}
			catch (ArgumentException)
			{
				return NotFound();
			}
			return NoContent();
		}
	}
}
