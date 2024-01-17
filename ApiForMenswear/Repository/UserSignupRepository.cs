using ApiForMenswear.Data;
using ApiForMenswear.Interface;
using ApiForMenswear.Models;
using ApiForMenswear.Utilities;

namespace ApiForMenswear.Repository
{
	public class UserSignupRepository : IUserSignupRepository
	{
		private readonly ApiContext _context;

		public UserSignupRepository(ApiContext context)
		{
			_context = context;
		}
		public void DeleteAllUsers()
		{
			_context.UserSignup.RemoveRange(_context.UserSignup);
			_context.SaveChanges();
		}

		public void DeleteSignup(int id)
		{
			var signupToRemove = _context.UserSignup.FirstOrDefault(s => s.Id == id);
			if (signupToRemove != null)
			{
				_context.UserSignup.Remove(signupToRemove);
				_context.SaveChanges();

			}
			else
			{
				throw new ArgumentException("Signup not found");
			}
		}

		public IEnumerable<UserSignup> GetAll()
		{
			var signup = _context.UserSignup.ToList();
			return signup;
		}

		public UserSignup GetByEmail(string email)
		{
			return _context.UserSignup.FirstOrDefault(u => u.Email == email);
		}

		public UserSignup GetById(int id)
		{
			return _context.UserSignup.FirstOrDefault(s => s.Id == id);
		}

		public void Insert(UserSignup signup)

		{
			signup.Password = PasswordUtilities.HashPassword(signup.Password);
			_context.UserSignup.Add(signup);
			_context.SaveChanges();
		}

		//public UserSignup Login(string usernameOrEmail, string password)
		//{
		//	throw new NotImplementedException();
		//}

		public void UpdateSignup(int id, UserSignup updatedSignup)
		{
			var existingSignup = _context.UserSignup.FirstOrDefault(s => s.Id == id);
			if (existingSignup != null)
			{
				existingSignup.Username = updatedSignup.Username ?? existingSignup.Username;
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
