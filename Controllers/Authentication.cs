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
        private readonly JwtService _jwtService;

        public Authentication(ECommerceAPI_DbContext dbContext, JwtService jwtService)
        {
            _dbContext = dbContext;
            _jwtService = jwtService;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> Signup(CustomerSignupDto requestModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var passwordHash = HashingService.CreatePasswordHash(requestModel.Password);

            var customer = new Customer
            {
                UserName = requestModel.UserName,
                UserEmail = requestModel.UserEmail,
                PasswordHash = passwordHash,
                PhoneNumber = requestModel.PhoneNumber,
            };

            _dbContext.Customers.Add(customer);
            await _dbContext.SaveChangesAsync();

            return Ok(new { Message = "Customer created successfully" });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(CustomerLoginDto requestModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var customer = await _dbContext.Customers
                .FirstOrDefaultAsync(c => c.UserEmail == requestModel.UserEmail);

            if (customer == null)
            {
                return Unauthorized(new { Message = "Invalid email or password" });
            }

            bool isPasswordValid = HashingService.VerifyPasswordHash(requestModel.Password, customer.PasswordHash);

            if (!isPasswordValid)
            {
                return Unauthorized(new { Message = "Invalid email or password" });
            }

            var token = _jwtService.GenerateSecurityToken(customer);

            return Ok(new { Token = token });
        }
    }
}
