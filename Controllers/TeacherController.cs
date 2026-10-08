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

        [HttpGet]
        public async Task<IActionResult> GetALlTeacherAsync()
        {
            var teachers = await _service.GetAllTeacherAsync();
            return Ok(teachers);
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetByIdAsync(Guid Id)
        {
            var teacher = await _service.GetTeacherByIdAsync(Id);
            return Ok(teacher);
        }

        [HttpPut]
        public async Task<IActionResult> Update(Guid Id, UpdateTeacherDto dto)
        {
            try
            {
                var teacher = await _service.Update(Id, dto);
                return Ok(teacher);
            }
            catch(KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete (Guid Id)
        {
            try
            {
                var teacher = await _service.Delete(Id);
                return Ok(teacher);
            }
            catch(KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
