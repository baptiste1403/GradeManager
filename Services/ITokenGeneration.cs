using GradeManager.Models;

namespace GradeManager.Services
{
    public interface ITokenGeneration
    {
        (string token, long expiresAt) GenerateToken(ApplicationUser user, IList<string> roles);
    }
}