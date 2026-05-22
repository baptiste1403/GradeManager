using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace GradeManager.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [StringLength(100, ErrorMessage = "First name cannot be longer than 100 characters.")]
        public string? FirstName { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "Last name cannot be longer than 100 characters.")]
        public string? LastName { get; set; }
    }
}