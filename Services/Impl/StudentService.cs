using GradeManager.Data;
using GradeManager.Models;
using Microsoft.EntityFrameworkCore;

namespace GradeManager.Services
{
    public class StudentService : IStudentService
    {

        private readonly ApplicationDbContext _context;
        public StudentService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Student>> GetStudentsForTeacherAsync(string teacherId)
        {
            var teacher = await _context.Teachers.FindAsync(teacherId) ?? throw new Exception("Teacher not found");
            return teacher.Students;
        }

        public async Task<IEnumerable<Student>> GetStudents() => await _context.Students.ToListAsync();

        public async Task<Student> GetStudent(string studentId) => await _context.Students.FindAsync(studentId) ?? throw new Exception("Student not found");

        public async Task AddGradeToStudentAsync(object studentId, int score, float coefficient)
        {
            var student = await _context.Students.FindAsync(studentId) ?? throw new Exception("Student not found");
            var grade = new Grade
            {
                Value = score,
                Coefficient = coefficient,
                Student = student,
                StudentId = student.Id
            };
            _context.Grades.Add(grade);
            student.Grades.Add(grade);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Grade>> GetGradesForStudentAsync(string studentId)
        {
            var student = await _context.Students.FindAsync(studentId) ?? throw new Exception("Student not found");
            return student.Grades;
        }
    }
}