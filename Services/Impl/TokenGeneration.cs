using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using GradeManager.Models;
using Microsoft.IdentityModel.Tokens;

namespace GradeManager.Services
{
    public class TokenGeneration : ITokenGeneration
    {

        private readonly IJWTConfigurationService _jWTConfigurationService;

        public TokenGeneration(IJWTConfigurationService jWTConfigurationService)
        {
            _jWTConfigurationService = jWTConfigurationService;
        }
        public (string token, long expiresAt) GenerateToken(ApplicationUser user, IList<string> roles)
        {
            var authClaims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, user.Id),
                new(ClaimTypes.Email, user.Email!),
                new("firstName", user.FirstName ?? string.Empty),
                new("lastName", user.LastName ?? string.Empty),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            // Append user roles to the claims list
            foreach (var role in roles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            // 3. Sign the Token
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jWTConfigurationService.GetJwtSecretKey()));

            var token = new JwtSecurityToken(
                issuer: _jWTConfigurationService.GetIssuer(),
                audience: _jWTConfigurationService.GetAudience(),
                expires: DateTime.UtcNow.AddMinutes(_jWTConfigurationService.GetJwtExpirationInMinutes()),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            return (new JwtSecurityTokenHandler().WriteToken(token), new DateTimeOffset(token.ValidTo).ToUnixTimeSeconds());
        }
    }
}