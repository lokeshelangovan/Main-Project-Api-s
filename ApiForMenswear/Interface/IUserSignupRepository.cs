using ApiForMenswear.Models;

namespace ApiForMenswear.Interface
{
	public interface IUserSignupRepository
	{
		IEnumerable<UserSignup> GetAll();
		UserSignup GetById(int id);
		void Insert(UserSignup signup);
		void UpdateSignup(int id, UserSignup updatedSignup);
		void DeleteSignup(int id);
		void DeleteAllUsers();		
		UserSignup GetByEmail(string email);

	}
}
