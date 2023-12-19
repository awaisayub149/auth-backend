using Microsoft.AspNetCore.Http; // Add this for HttpContext
using Microsoft.AspNetCore.Mvc;
using webapi.Models;
using webapi.Services;

namespace webapi.Controllers
{
    [Route("api/protected/users")]
    [ApiController]
    public class protectedController : ControllerBase
    {
        private readonly TokenService _tokenService;
        private readonly userService _uService;
        private IConfiguration _config;

        public protectedController(userService uService, IConfiguration config)
        {
            _uService = uService;
            _config = config;
            _tokenService = new TokenService("ADMIN125125125!@#"); // Replace with your actual secret key
        }

        [HttpGet]
        public ActionResult<List<Userlist>> GetUser()
        {
            var token = HttpContext
                .Request.Headers["Authorization"]
                .FirstOrDefault()
                ?.Split(" ")
                .Last();

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
