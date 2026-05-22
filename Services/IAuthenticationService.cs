using GradeManager.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GradeManager.Services
{
    public interface IAuthService
    {
        Task<IdentityResult> RegisterUserWithRolesAsync(ApplicationUser user, string password, string role);
        Task<LoginResultDTO> LoginUser(string email, string password);

        Task AssignRoleToUser(string userEmail, string roleName);
    }
}