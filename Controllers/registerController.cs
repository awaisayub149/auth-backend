using Microsoft.AspNetCore.Mvc;
using webapi.Models;
using webapi.Services;

namespace webapi.Controllers
{
    [Route("api/public/register")]
    [ApiController]

    // RegisterController handles user registration operations
    public class registerController : ControllerBase
    {
        private readonly userService _uService;

        // Constructor to inject userService and configuration dependencies
        public registerController(userService uService)
        {
            _uService = uService;
        }

        // IActionResult used to return different HTTP responses
        [HttpPost]
        public IActionResult Post([FromBody] Userlist userlist)
        {
            if (userlist == null)
            {
                return BadRequest("Invalid user data");
            }

            // Extract user details from the userlist model
            string name = userlist.Name;
            string email = userlist.Email;
            string password = userlist.Password;

            // You may want to add validation logic for email and password here

            // Call the CreateUser method from the service
            _uService.CreateUser(name, email, password);

            // Retrieve the newly created user
            var newUser = _uService.GetUser(email);

            if (newUser == null)
            {
                return StatusCode(500, "Error retrieving the newly created user");
            }
            // Return the newly created user in the response
            return Ok(newUser);
        }
    }
}
