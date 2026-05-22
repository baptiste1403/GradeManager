using System.IdentityModel.Tokens.Jwt;
using System.Runtime.InteropServices;
using System.Security.Claims;
using GradeManager.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GradeManager.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly ITokenGeneration _tokenGeneration;

        public AuthService(UserManager<ApplicationUser> userManager, ITokenGeneration tokenGeneration)
        {
            _userManager = userManager;
            _tokenGeneration = tokenGeneration;
        }

        public async Task<IdentityResult> RegisterUserWithRolesAsync(ApplicationUser user, string password, string role)
        {
            var createResult = await _userManager.CreateAsync(user, password);

            if (!createResult.Succeeded)
            {
                return createResult;
            }

            var roleResult = await _userManager.AddToRoleAsync(user, role);

            if(!roleResult.Succeeded)
            {
                await _userManager.DeleteAsync(user);
                return roleResult;
            }

            return IdentityResult.Success;
        }

        public async Task<LoginResultDTO> LoginUser(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if(user == null || !await _userManager.CheckPasswordAsync(user, password))
            {
                return new LoginResultDTO
                {
                    IsSuccess = false,
                    ErrorMessage = "Invalid login attempt."
                };
            }

            // Append user roles to the claims list
            var userRoles = await _userManager.GetRolesAsync(user);
            
            var (token, expiresAt) = _tokenGeneration.GenerateToken(user, userRoles);

            return new LoginResultDTO
            {
                IsSuccess = true,
                Token = token,
                ExpiresAt = expiresAt
            };
        }

        public async Task AssignRoleToUser(string userEmail, string roleName)
        {

            var user = await _userManager.FindByEmailAsync(userEmail);
            if(user == null)
            {
                throw new Exception($"User with email {userEmail} not found.");
            }
            var result = await _userManager.AddToRoleAsync(user, roleName);
            if(!result.Succeeded)
            {
                throw new Exception($"Failed to assign role {roleName} to user {userEmail}: {string.Join(", ", result.Errors.Select(e => e.Description))}");
            }
        }
    }
}