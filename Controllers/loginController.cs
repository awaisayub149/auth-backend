using Microsoft.AspNetCore.Mvc;
using webapi.Models;
using webapi.Services;

namespace webapi.Controllers
{
    [Route("api/public/login")]
    [ApiController]
    public class loginController : ControllerBase
    {
        private readonly userService _uService;

        public loginController(userService uService)
        {
            _uService = uService;
        }

        [HttpPost]
        public IActionResult Post([FromBody] Userlist userlist)
        {
            //your logic for login process
            //If login usrename and password are correct then proceed to generate token
            var token = _uService.ValidateField(userlist.Email, userlist.Password);
            if (token == null)
            {
                return Unauthorized();
            }

            var newUser = _uService.GetUser(userlist.Email);
            return Ok(new { Token = token, User = newUser });
        }
    }
}
