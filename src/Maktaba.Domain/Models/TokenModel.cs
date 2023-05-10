namespace Maktaba.Domain;

public class TokenModel
{
    public string Token { get; set; } = null!;
    public bool Success { get; set; }
    public DateTime Expiration { get; set; }
}