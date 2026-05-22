using GradeManager.Data;

namespace GradeManager.Services
{
    public class TeacherService : ITeacherService
    {
        private readonly ApplicationDbContext _context;

        public TeacherService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AssignStudentToTeacherAsync(string studentId, string teacherId)
        {
            var student = await _context.Students.FindAsync(studentId) ?? throw new Exception("Student not found");
            var teacher = await _context.Teachers.FindAsync(teacherId) ?? throw new Exception("Teacher not found");

            if(student.TeacherId != null)
            {
                throw new Exception("Student is already assigned to a teacher");
            }

            student.Teacher = teacher;
            student.TeacherId = teacherId;
            teacher.Students.Add(student);
            await _context.SaveChangesAsync();
        }
    }
}