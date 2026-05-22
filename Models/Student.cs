namespace GradeManager.Models
{
    public class Student : ApplicationUser
    {
        public virtual ICollection<Grade> Grades { get; set; } = [];

        public string? TeacherId { get; set; }
        public virtual Teacher? Teacher { get; set; }
    }
}