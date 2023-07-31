using Microsoft.AspNetCore.Mvc;
using Terminal.DTOs;
using Terminal.Models;
using TerminalAPI.Services;

namespace TerminalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthServices _authServices;

        public AuthController(IAuthServices authServices)
        {
            _authServices = authServices;
        }

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(UserDTO userDTO)
        {
            try
            {
                User user = _authServices.RegisterUser(userDTO);
                return Ok(user);
            }
            catch
            {
                return BadRequest("The request user could not be registered due to internal server issues");
            }
        }
    }
}
