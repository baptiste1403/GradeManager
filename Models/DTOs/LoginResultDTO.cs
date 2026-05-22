namespace GradeManager.Models
{
    public class LoginResultDTO
    {
        public bool IsSuccess { get; set; }
        public string Token { get; set; } = string.Empty;
        public long ExpiresAt { get; set; }
        public string ErrorMessage { get; set; } = string.Empty;
    }
}