using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolApi.Application.DTOS.StudentDto;
using SchoolApi.Application.Interfaces.IServices;

namespace SchoolApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentservice;

        public StudentController(IStudentService studentService)
        {
            _studentservice = studentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStudents()
        {
            var students = await _studentservice.GetAllStudent();
            return Ok(students);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetStudentByIdAsync(Guid Id)
        {
            var student = await _studentservice.GetStudentByIdAsync(Id);
            return Ok(student);
        }

        [HttpPost]
        public async Task<IActionResult> AddStudentAsync(CreateStudentDto dto)
        {
            var student = await _studentservice.AddStudentAsync(dto);
            return Ok(student);
        }

        [HttpPut]
        public async Task<IActionResult> Update(Guid Id, UpdateStudentDto dto)
        {
            var student = await _studentservice.Update(Id, dto);
            return Ok(student);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid Id)
        {
            var student = await _studentservice.Delete(Id);
            return Ok(student);
        }



    }
}
