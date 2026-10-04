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

        [HttpPost]
        public async Task<IActionResult> RegisterAsync(RegisterDto dto)
        {

             await _authService.RegisterAsync(dto);
            return Ok();
        }
    }
    
}
