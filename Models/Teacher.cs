namespace GradeManager.Models
{
    public class Teacher : ApplicationUser
    {
        public virtual ICollection<Student> Students { get; set; } = [];
    }
}