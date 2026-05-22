namespace GradeManager.Controllers
{
    using GradeManager.Models;
    using GradeManager.Services;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;
        private readonly IUserContext _userContext;

        public StudentController(IStudentService studentService, IUserContext userContext)
        {
            _studentService = studentService;
            _userContext = userContext;
        }

        [HttpGet("all")]
        [Authorize(Roles = "Student,Teacher")]
        public async Task<IActionResult> GetAllStudents()
        {
            var students = await _studentService.GetStudents();
            var studentDTOs = students.Select(StudentMapper.ToDTO);
            return Ok(studentDTOs);
        }

        [HttpPost("add-grade")]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> AddGrade([FromBody] AddGradeDTO dto)
        {
            var teacherId = _userContext.UserId;
            if (string.IsNullOrEmpty(teacherId))
            {
                return Unauthorized("User ID not found in token.");
            }
            var student = await _studentService.GetStudent(dto.StudentId);
            if(student == null)
            {
                return NotFound("Student not found.");
            }
            if(student.TeacherId != teacherId)
            {
                return Forbid("You can only add grades to your own students.");
            }
            try
            {
                await _studentService.AddGradeToStudentAsync(dto.StudentId, dto.Value, dto.Coefficient);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("grades")]
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> GetGradesForStudent()
        {
            var studentId = _userContext.UserId;
            if (string.IsNullOrEmpty(studentId)) 
            {
                return Unauthorized("User ID not found in token.");
            }
            var grades = await _studentService.GetGradesForStudentAsync(studentId);
            var gradeDTOs = grades.Select(GradeMapper.ToDTO);
            return Ok(gradeDTOs);
        }
    }
}