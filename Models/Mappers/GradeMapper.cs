namespace GradeManager.Models
{
    public class GradeMapper
    {
        public static GradeDTO ToDTO(Grade grade)
        {
            return new GradeDTO
            {
                Value = grade.Value,
                Coefficient = grade.Coefficient
            };
        }
    }
}