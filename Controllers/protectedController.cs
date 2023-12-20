using Microsoft.AspNetCore.Mvc;
using webapi.Models;
using webapi.Services;


// Define the namespace and class for the protected controller
namespace webapi.Controllers
{
    [Route("api/protected/users")]
    [ApiController]

    public class protectedController : ControllerBase
    {
        private readonly TokenService _tokenService;
        private readonly userService _uService;

        // Constructor to inject userService and configuration dependencies
        public protectedController(userService uService)
        {
            _uService = uService;
            _tokenService = new TokenService("ADMIN125125125!@#"); // Replace with your actual secret key
        }

        [HttpGet]
        public ActionResult<List<Userlist>> GetUser()
        {
            // Retrieve the token from the request header
            var token = HttpContext
                .Request.Headers["Authorization"]
                .FirstOrDefault()
                ?.Split(" ")
                .Last();
            // If the token is missing or empty, return Unauthorized
            if (string.IsNullOrEmpty(token))
            {
                // Token is missing or invalid, return Unauthorized
                return Unauthorized("Token is missing or invalid");
            }

            Console.WriteLine(token, "This is the token");
            var principal = _tokenService.ValidateToken(token);

            if (principal == null)
            {
                // Token validation failed, return Unauthorized
                return Unauthorized("Token validation failed");
            }

            return _uService.GetUser();
        }
    }
}
