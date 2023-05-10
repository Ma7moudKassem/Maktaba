namespace Maktaba.Infrastructure;

public class MaktabaTokenValidationParameters : TokenValidationParameters
{
    public MaktabaTokenValidationParameters(IConfiguration configuration)
    {
        ValidIssuer = configuration["JWT:Issuer"];
        ValidAudience = configuration["JWT:Audience"];
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]));
    }
}
