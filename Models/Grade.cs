namespace GradeManager.Models
{
    public class Grade
    {
        public string Id { get; init; } = Guid.NewGuid().ToString();
        
        public required int Value { get; set; }

        public required float Coefficient { get; set; }

        public required string StudentId { get; set; }
        public virtual required Student Student { get; set; }
    }
}