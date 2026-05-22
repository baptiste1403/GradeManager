namespace GradeManager.Models
{
    public class StudentDTO
    {
        public required string Id { get; set; }
        public required string Email { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }

        public required string TeacherId { get; set; }
    }
}