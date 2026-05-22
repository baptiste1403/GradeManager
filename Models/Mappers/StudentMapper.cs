namespace GradeManager.Models
{
    public class StudentMapper
    {
        public static StudentDTO ToDTO(Student student)
        {
            return new StudentDTO
            {
                Id = student.Id,
                Email = student.Email ?? throw new InvalidOperationException("Student email cannot be null"),
                FirstName = student.FirstName ?? throw new InvalidOperationException("Student first name cannot be null"),
                LastName = student.LastName ?? throw new InvalidOperationException("Student last name cannot be null"),
                TeacherId = student.TeacherId ?? "no teacher assigned"
            };
        }
    }
}