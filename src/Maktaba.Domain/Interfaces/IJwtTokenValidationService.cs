namespace Maktaba.Domain;

public interface IJwtTokenValidationService
{
    Task<TokenModel> GenerateTokenModelAsync(CredentialModel model);
}