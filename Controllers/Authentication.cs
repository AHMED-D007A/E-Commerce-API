using E_Commerce_API.Dtos.Customer;
using E_Commerce_API.Models;
using E_Commerce_API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_API.Controllers
{
    [Route("api")]
    [ApiController]
    public class Authentication : ControllerBase
    {
        private readonly ECommerceAPI_DbContext _dbContext;

        public Authentication(ECommerceAPI_DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> Signup(CustomerSignupDto requestModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            HashingService.CreatePasswordHash(requestModel.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var customer = new Customer
            {
                UserName = requestModel.UserName,
                UserEmail = requestModel.UserEmail,
                PasswordHash = Convert.ToBase64String(passwordHash),
                PhoneNumber = requestModel.PhoneNumber,
            };

            _dbContext.Customers.Add(customer);
            await _dbContext.SaveChangesAsync();

            return Ok(new { Message = "Customer created successfully" });
        }

        [HttpPost("login")]
        public IActionResult Login(CustomerLoginDto requestModel)
        {
            Console.WriteLine(requestModel.UserEmail);
            return Ok();
        }
    }
}
