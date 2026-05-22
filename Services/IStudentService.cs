using GradeManager.Models;

namespace GradeManager.Services
{
    public interface IStudentService
    {
        Task<IEnumerable<Student>> GetStudents();
        Task<Student> GetStudent(string studentId);
        Task AddGradeToStudentAsync(object studentId, int score, float coefficient);
        Task<IEnumerable<Student>> GetStudentsForTeacherAsync(string teacherId);
        Task<IEnumerable<Grade>> GetGradesForStudentAsync(string studentId);
    }
}