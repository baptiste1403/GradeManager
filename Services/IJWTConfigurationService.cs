namespace GradeManager.Services
{
    public interface IJWTConfigurationService
    {
        string GetJwtSecretKey();
        int GetJwtExpirationInMinutes();

        string GetIssuer();
        string GetAudience();
    }
}