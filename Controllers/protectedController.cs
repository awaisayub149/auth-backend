using Microsoft.AspNetCore.Http; // Add this for HttpContext
using Microsoft.AspNetCore.Mvc;
using webapi.Models;
using Microsoft.AspNetCore.Authorization;
using webapi.Services;
namespace webapi.Controllers
{
    [Route("api/protected/users")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")] // Add this attribute
    public class protectedController : ControllerBase
    {
        private readonly userService _uService;
        private IConfiguration _config;
        public protectedController(userService uService, IConfiguration config)
        {
            _uService = uService;
            _config = config;
            // _tokenService = new TokenService("ADMIN125125125!@#"); // Replace with your actual secret key
        }
        [HttpGet]
        public ActionResult<List<Userlist>> GetUser()
        {
            return _uService.GetUser();
        }
    }
}




