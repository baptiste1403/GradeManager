namespace GradeManager.Services
{
    public class JWTConfigurationService : IJWTConfigurationService
    {

        private readonly IConfiguration _configuration;

        public JWTConfigurationService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string GetAudience()
        {
            return _configuration.GetSection("Jwt").GetValue<string>("Audience") ?? 
            throw new InvalidOperationException("JWT Audience is not configured.");
        }

        public string GetIssuer()
        {
            return _configuration.GetSection("Jwt").GetValue<string>("Issuer") ?? 
            throw new InvalidOperationException("JWT Issuer is not configured.");
        }

        public int GetJwtExpirationInMinutes()
        {
            return _configuration.GetSection("Jwt").GetValue<int?>("Lifetime") ?? 
            throw new InvalidOperationException("JWT Expiration is not configured.");
        }

        public string GetJwtSecretKey()
        {
            return _configuration.GetSection("Jwt").GetValue<string>("Key") ?? 
            throw new InvalidOperationException("JWT Secret Key is not configured.");
        }
    }
}