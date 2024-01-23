using System.Text;
using System.Security.Cryptography;
namespace ApiForMenswear.Utilities
{
	public class PasswordUtilities
	{
		//------------------------------------Password Hashing-------------------------------------
		//public static string HashPassword(string password)
		//{
		//	using SHA256 sha256 = SHA256.Create();

		//	// Compute hash from password
		//	byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

		//	// Convert byte array to a hexadecimal string
		//	string hashedPassword = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();

		//	return hashedPassword;
		//}

		public static string HashPassword(string password)
		{
			return BCrypt.Net.BCrypt.HashPassword(password);
		}

		public static bool VerifyPassword(string enteredPassword, string hashedPassword)
		{
			return BCrypt.Net.BCrypt.Verify(enteredPassword, hashedPassword);
		}
	}
}
