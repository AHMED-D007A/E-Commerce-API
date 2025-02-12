using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using E_Commerce_API.Models;
using E_Commerce_API.Dtos.Customer;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace E_Commerce_API.Controllers
{
    [Route("api/profile")]
    [ApiController]
    [Authorize]
    public class Profile : ControllerBase
    {
        private readonly ECommerceAPI_DbContext _dbContext;

        public Profile(ECommerceAPI_DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        private string GetUserEmail()
        {
            User.Claims.ToList().ForEach(c => Console.WriteLine($"{c.Type}: {c.Value}"));
            var emailClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return emailClaim!;
        }

        [HttpGet]
        public async Task<IActionResult> GetProfile()
        {
            var userEmail = GetUserEmail();

            if (string.IsNullOrEmpty(userEmail))
            {
                return Unauthorized();
            }

            var customer = await _dbContext.Customers.FirstOrDefaultAsync(c => c.UserEmail == userEmail);

            if (customer == null)
            {
                return NotFound(new { Message = "Customer not found" });
            }

            var ProfileDto = new ProfileDto
            {
                UserName = customer.UserName,
                UserEmail = customer.UserEmail,
                PhoneNumber = customer.PhoneNumber,
                Addresses = customer.Addresses,
                Favorites = customer.Favorites
            };

            return Ok(ProfileDto);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProfile([FromBody] ProfileUpdateDto requestModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userEmail = GetUserEmail();

            if (string.IsNullOrEmpty(userEmail))
            {
                return Unauthorized();
            }

            var customer = await _dbContext.Customers.FirstOrDefaultAsync(c => c.UserEmail == userEmail);

            if (customer == null)
            {
                return NotFound(new { Message = "Customer not found" });
            }

            customer.UserName = requestModel.UserName;
            customer.PhoneNumber = requestModel.PhoneNumber;

            _dbContext.Customers.Update(customer);
            await _dbContext.SaveChangesAsync();

            return Ok(new { Message = "Profile updated successfully" });
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteProfile()
        {
            var userEmail = GetUserEmail();

            if (string.IsNullOrEmpty(userEmail))
            {
                return Unauthorized();
            }

            var customer = await _dbContext.Customers.FirstOrDefaultAsync(c => c.UserEmail == userEmail);

            if (customer == null)
            {
                return NotFound(new { Message = "Customer not found" });
            }

            _dbContext.Customers.Remove(customer);
            await _dbContext.SaveChangesAsync();

            return Ok(new { Message = "Profile deleted successfully" });
        }
    }
}