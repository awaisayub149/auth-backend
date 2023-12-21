using BCrypt.Net;
using Microsoft.AspNetCore.Mvc;
using webapi.Models;
using webapi.Services;

namespace webapi.Controllers
{
    [Route("api/public/register")]
    [ApiController]
    public class registerController : ControllerBase
    {
        private readonly userService _uService;
        private IConfiguration _config;

        public registerController(userService uService, IConfiguration config)
        {
            _uService = uService;
            _config = config;
        }

        [HttpPost]
        public IActionResult Post([FromBody] Userlist userlist)
        {
            if (userlist == null)
            {
                return BadRequest("Invalid user data");
            }

            // Assuming you have Name, Email, and Password in the Userlist model
            string name = userlist.Name;
            string email = userlist.Email;
            string password = userlist.Password;

            // You may want to add validation logic for email and password here
            // Hash the password using BCrypt
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

            // Call the CreateUser method from the service
            _uService.CreateUser(name, email, hashedPassword);

            // Retrieve the newly created user
            var newUser = _uService.GetUser(email);

            if (newUser == null)
            {
                return StatusCode(500, "Error retrieving the newly created user");
            }

            return Ok(newUser);
        }
    }
}