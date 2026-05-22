using GradeManager.Models;
using GradeManager.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GradeManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Teacher")]
    public class TeacherController : ControllerBase
    {

        private readonly ITeacherService _teacherService;
        private readonly IStudentService _studentService;
        private readonly IUserContext _userContext;

        public TeacherController(ITeacherService teacherService, IStudentService studentService, IUserContext userContext)
        {
            _teacherService = teacherService;
            _studentService = studentService;
            _userContext = userContext;
        }

        [HttpGet("students")]
        public async Task<IActionResult> GetStudents()
        {
            var teacherId = _userContext.UserId;
            if (string.IsNullOrEmpty(teacherId))
            {
                return Unauthorized("User ID not found in token.");
            }
            var students = await _studentService.GetStudentsForTeacherAsync(teacherId);
            var studentDTOs = students.Select(StudentMapper.ToDTO);
            return Ok(studentDTOs);
        }

        [HttpPost("assign-student")]
        public async Task<IActionResult> AssignStudent([FromBody] AssignStudentDTO dto)
        {
            var teacherId = _userContext.UserId;
            if (string.IsNullOrEmpty(teacherId))
            {
                return Unauthorized("User ID not found in token.");
            }
            try
            {
                await _teacherService.AssignStudentToTeacherAsync(dto.StudentId, teacherId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}