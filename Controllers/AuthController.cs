using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolApi.Application.DTOS.UserDto;
using SchoolApi.Application.Interfaces.IServices;

namespace SchoolApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterAsync(RegisterDto dto)
        {
            try
            {
                await _authService.RegisterAsync(dto);
                return Ok("Login Succesful");
            }
            catch (ArgumentException ex)
            {
               return BadRequest(ex.Message);
            }

            catch(InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }

        }

        [HttpPost("{Login}")]
        public async Task<IActionResult> LoginAsync(LoginDto dto)
        {
            try
            {
                await _authService.LoginAsync(dto);
                return Ok("Login Succesful");
            }
            catch(UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
        }
    }
    
}
