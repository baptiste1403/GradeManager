using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GradeManager.Models
{
    public class Grade
    {
        [Key]
        public string Id { get; init; } = Guid.NewGuid().ToString();
        
        [Required]
        [Range(0, 20, ErrorMessage = "Grade value must be between 0 and 20.")]
        public required int Value { get; set; }

        [Required]
        [Range(0, 10, ErrorMessage = "Coefficient must be between 0 and 10.")]
        public required float Coefficient { get; set; }

        [Required]
        public required string StudentId { get; set; }

        [Required]
        public virtual required Student Student { get; set; }
    }
}