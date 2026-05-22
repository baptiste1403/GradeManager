using GradeManager.Models;
using GradeManager.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace GradeManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {

        private readonly IAuthService _authService;

        public AdminController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("assign-role")]
        public async Task<IActionResult> AssignRole([FromBody] AssignRoleDTO dto)
        {
            await _authService.AssignRoleToUser(dto.UserEmail, dto.RoleName);
            return Ok($"Role {dto.RoleName} assigned to user {dto.UserEmail} successfully.");
        }
    }
}