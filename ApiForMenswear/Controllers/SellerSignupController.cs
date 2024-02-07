using ApiForMenswear.Interface;
using ApiForMenswear.Models;
using Microsoft.AspNetCore.Mvc;
using ApiForMenswear.Utilities;


namespace ApiForMenswear.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class SellerSignupController : ControllerBase
	{
		private readonly IUserSignupRepository _userSignupRepository;
		private readonly ISellerSignupRepository _sellerSignupRepository;
		private readonly IProductManagementRepository _productManagementRepository;
		private readonly IOrderRepository _orderRepository;
		

		public SellerSignupController( ISellerSignupRepository sellerSignupRepository)
		{
			
			_sellerSignupRepository = sellerSignupRepository;
			
		}
		
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
				
				var existingSignup = _sellerSignupRepository.GetById(id);

				if (existingSignup != null)
				{
					
					if (!string.IsNullOrEmpty(signup.Password))
					{
						
						string hashedPassword = PasswordUtilities.HashPassword(signup.Password);

						
						existingSignup.Password = hashedPassword;
					}

					
					existingSignup.Name = signup.Name ?? existingSignup.Name;
					existingSignup.Email = signup.Email ?? existingSignup.Email;

					
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
				
				return Ok(new { Message = "Login Successful" });
			}
			else
			{
				
				return Unauthorized("Invalid email or password.");
			}
		}
		
		
	}
}
