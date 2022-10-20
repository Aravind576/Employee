using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer;
using RepositoryLayer;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly EmployeeContext _employeeContext;
        public UserController(EmployeeContext employeeContext)
        {
                _employeeContext = employeeContext;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(AccountRegister request)
        {
            if (_employeeContext.accountDetails.Any(u => u.UserName == request.UserName))
            {
                return BadRequest("User already exist");
            }

            CreatePasswordHash(request.Password
                , out byte[] passwordHash
                , out byte[] passwordSalt);

            var user = new AccountDetails
            {
                UserName = request.UserName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                
            };

            _employeeContext.accountDetails.Add(user);
            await _employeeContext.SaveChangesAsync();

            return Ok("user Succesfully created");

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(AccountLogin request)
        {

            var user = await _employeeContext.accountDetails.FirstOrDefaultAsync(u => u.UserName == request.UserName);
            if (user == null)
            {
                return BadRequest("User not found");
            }

            if (!VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
            {
                return BadRequest("Password is Incorrect");
            }

            


            return Ok($"Welcome, {user.UserName}!:)");


        }

        





        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.
                    ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.
                    ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);

            }
        }

    }
}
