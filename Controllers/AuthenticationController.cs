using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using GradeManager.Models;
using GradeManager.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace GradeManager.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class AuthController : ControllerBase
{

    private readonly IAuthService _authenticationService;

    // Inject UserManager and SignInManager via constructor
    public AuthController(IAuthService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [Authorize(Roles = "Admin")] // Ensure this endpoint is protected if needed
    [HttpPost("register-student")] // Route: POST api/auth/register
    public async Task<IActionResult> RegisterStudent([FromBody] RegisterUserDTO dto)
    {
        // 1. Validate incoming data
        if (string.IsNullOrEmpty(dto.Email) || 
            string.IsNullOrEmpty(dto.Password) || 
            string.IsNullOrEmpty(dto.FirstName) || 
            string.IsNullOrEmpty(dto.LastName))
        {
            return BadRequest("All fields are required.");
        }

        // 2. Map DTO to ApplicationUser object
        var user = new Student
        {
            UserName = dto.Email,
            Email = dto.Email,
            FirstName = dto.FirstName,
            LastName = dto.LastName
        };

        var result = await _authenticationService.RegisterUserWithRolesAsync(user, dto.Password, "Student");
        if (!result.Succeeded)
        {
            // Convert Identity errors to a dictionary for ValidationProblem
            var errorDictionary = result.Errors.ToDictionary(
                e => e.Code,
                e => new[] { e.Description }
            );

            return ValidationProblem(new ValidationProblemDetails(errorDictionary));
        }

        return Ok(new { Message = "User registered successfully!" });
    }

    [Authorize(Roles = "Admin")] // Ensure this endpoint is protected if needed
    [HttpPost("register-teacher")] // Route: POST api/auth/register
    public async Task<IActionResult> RegisterTeacher([FromBody] RegisterUserDTO dto)
    {
        // 1. Validate incoming data
        if (string.IsNullOrEmpty(dto.Email) || 
            string.IsNullOrEmpty(dto.Password) || 
            string.IsNullOrEmpty(dto.FirstName) || 
            string.IsNullOrEmpty(dto.LastName))
        {
            return BadRequest("All fields are required.");
        }

        // 2. Map DTO to ApplicationUser object
        var user = new Teacher
        {
            UserName = dto.Email,
            Email = dto.Email,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
        };

        var result = await _authenticationService.RegisterUserWithRolesAsync(user, dto.Password, "Teacher");
        if (!result.Succeeded)
        {
            // Convert Identity errors to a dictionary for ValidationProblem
            var errorDictionary = result.Errors.ToDictionary(
                e => e.Code,
                e => new[] { e.Description }
            );

            return ValidationProblem(new ValidationProblemDetails(errorDictionary));
        }

        return Ok(new { Message = "User registered successfully!" });
    }

    [AllowAnonymous] // Allow unauthenticated users to access the login endpoint
    [HttpPost("login")] // Route: POST api/auth/login
    public async Task<IActionResult> Login([FromBody] LoginUserDTO model)
    {
        if (string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Password))
        {
            return BadRequest("Email and Password are required.");
        }

        var loginResult = await _authenticationService.LoginUser(model.Email, model.Password);
        if (!loginResult.IsSuccess)
        {
            return Unauthorized(loginResult.ErrorMessage);
        }
        return Ok(new 
        {
            loginResult.Token,
            loginResult.ExpiresAt 
        });
    }
}