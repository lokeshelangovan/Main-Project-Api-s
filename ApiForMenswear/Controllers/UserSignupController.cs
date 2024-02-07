using ApiForMenswear.Interface;
using ApiForMenswear.Models;
using ApiForMenswear.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace ApiForMenswear.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserSignupController : ControllerBase
    {
        private readonly IUserSignupRepository _userSignupRepository;

        public UserSignupController(IUserSignupRepository userSignupRepository)
        {
           _userSignupRepository = userSignupRepository;
        }

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
                // Generate a token or any other necessary authentication logic if needed

                // Return user details including the userId
                return Ok(new { UserId = user.Id, Email = user.Email });
            }
            else
            {
                return Unauthorized("Invalid email or password.");
            }
        }



    }
}
