using ApiForMenswear.Models;

namespace ApiForMenswear.Interface
{
	public interface ISellerSignupRepository
	{
		IEnumerable<SellerSignup> GetAll();
		SellerSignup GetById(int id);
		void Insert(SellerSignup signup);
		void UpdateSignup(int id, SellerSignup updatedSignup);
		void DeleteSignup(int id);

		SellerSignup GetByEmail(string email);
	}
}
