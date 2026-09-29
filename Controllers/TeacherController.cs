using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolApi.Application.DTOS.TeacherDto;
using SchoolApi.Application.Interfaces.IServices;
using SchoolApi.Application.Services;

namespace SchoolApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherService _service;
        public TeacherController(ITeacherService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> AddteacherAsync(CreateTeacherDto dto)
        {
            var teacher = await _service.AddTeacherAsync(dto);
            return Ok(teacher);
        }

    }
}
