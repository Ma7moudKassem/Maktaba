﻿using System.Text.Json.Serialization;

namespace Maktaba.Services.Identity.Domain;

public class AuthModel
{
    public string? Message { get; set; }
    public bool IsAuthenticated { get; set; }
    public string? Username { get; set; }
    public string? Email { get; set; }
    public string FullAddress { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public List<string>? Roles { get; set; }
    public string? Token { get; set; }
    public DateTime? ExpiresOn { get; set; }

    [JsonIgnore]
    public string? RefreshToken { get; set; }

    public DateTime RefreshTokenExpiration { get; set; }
}
