namespace GradeManager.Models
{
    public class AddGradeDTO
    {
        public required string StudentId { get; set; }
        public required int Value { get; set; }
        public required float Coefficient { get; set; }
    }
}