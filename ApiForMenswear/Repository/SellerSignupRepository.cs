using ApiForMenswear.Data;
using ApiForMenswear.Interface;
using ApiForMenswear.Models;
using ApiForMenswear.Utilities;

namespace ApiForMenswear.Repository
{
	public class SellerSignupRepository : ISellerSignupRepository
	{
		private readonly MenswearContext _context;

		public SellerSignupRepository(MenswearContext context)
		{
			_context = context;
		}
		public void DeleteSignup(int id)
		{
			var signupToRemove = _context.SellerSignup.FirstOrDefault(s => s.Id == id);
			if (signupToRemove != null)
			{
				_context.SellerSignup.Remove(signupToRemove);
				_context.SaveChanges();

			}
			else
			{
				throw new ArgumentException("Signup not found");
			}
		}

		public IEnumerable<SellerSignup> GetAll()
		{
			var signup = _context.SellerSignup.ToList();
			return signup;
		}

		public SellerSignup GetByEmail(string email)
		{
			return _context.SellerSignup.FirstOrDefault(u => u.Email == email);
		}

		public SellerSignup GetById(int id)
		{
			return _context.SellerSignup.FirstOrDefault(s => s.Id == id);
		}

		public void Insert(SellerSignup signup)
		{
			signup.Password = PasswordUtilities.HashPassword(signup.Password);
			_context.SellerSignup.Add(signup);
			_context.SaveChanges();
		}

		public void UpdateSignup(int id, SellerSignup updatedSignup)
		{
			var existingSignup = _context.SellerSignup.FirstOrDefault(s => s.Id == id);
			if (existingSignup != null)
			{
				existingSignup.Name = updatedSignup.Name ?? existingSignup.Name;
				existingSignup.Email = updatedSignup.Email ?? existingSignup.Email;
				existingSignup.Password = updatedSignup.Password ?? existingSignup.Password;

				_context.SaveChanges();
			}
			else
			{
				throw new ArgumentException("Signup not found");
			}
		}
	}
}
