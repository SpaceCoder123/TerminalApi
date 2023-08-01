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
        public async Task<ActionResult<string>> Register(UserDTO userDTO)
        {
            try
            {
                string token = await _authServices.RegisterUser(userDTO);
                return Ok(token);
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Login")]
        public async Task<ActionResult<string>> Login(UserLoginDTO userDTO)
        {
            try
            {
                return Ok(await _authServices.Login(userDTO));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Verify")]
        public async Task<ActionResult<string>> Verify(string token)
        {
            try
            {
                return Ok(await _authServices.Verify(token));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
