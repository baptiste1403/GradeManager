using GradeManager.Models;

namespace GradeManager.Services
{
    public interface ITeacherService
    {
        Task AssignStudentToTeacherAsync(string studentId, string teacherId);
    }
}