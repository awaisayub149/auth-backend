using Microsoft.AspNetCore.Mvc;
using webapi.Models;
using webapi.Services;

// Define the namespace and class for the login controller
namespace webapi.Controllers
{
    [Route("api/public/login")]
    [ApiController]
    // LoginController handles authentication for public users
    public class loginController : ControllerBase
    {
        private readonly userService _uService;
        // Constructor to inject userService     // Summarydependency
        public loginController(userService uService)
        {
            _uService = uService;
        }
        // HTTP POST endpoint for user login
        [HttpPost]
        // IActionResult used to return different HTTP responses
        public IActionResult Post([FromBody] Userlist userlist)
        {
            //your logic for login process
            //If login usrename and password are correct then proceed to generate token
            var token = _uService.ValidateField(userlist.Email, userlist.Password);
            if (token == null)
            {
                return Unauthorized();
            }

            // Retrieve user information after successful authentication    
            var newUser = _uService.GetUser(userlist.Email);
            return Ok(new { Token = token, User = newUser });
        }
    }
}
