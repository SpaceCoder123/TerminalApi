using Microsoft.AspNetCore.Mvc;
using Terminal.DTOs;
using Terminal.Models;
using TerminalAPI.Services;

namespace TerminalAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthServices _authServices;

        public AuthController(IAuthServices authServices)
        {
            _authServices = authServices;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<User>> Register(UserDTO userDTO)
        {
            try
            {
                User user = _authServices.RegisterUser(userDTO);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Login")]
        public async Task<ActionResult<string>> Login(UserDTO userDTO)
        {
            try
            {
                string token = _authServices.Login(userDTO);
                return Ok(token);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
