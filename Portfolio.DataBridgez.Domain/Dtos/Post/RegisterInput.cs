using System.Text.Json.Serialization;

namespace Portfolio.Databridgez.Domain.Dtos.Post;

public sealed class RegisterInput
{
     [JsonInclude]
     [JsonPropertyName(nameof(UserName))]
     public string? UserName { get; set; }
     
     [JsonInclude]
     [JsonPropertyName(nameof(Email))]
     public string? Email { get; set; }
     
     [JsonInclude]
     [JsonPropertyName(nameof(Password))]
     public string? Password { get; set; }
     
     [JsonInclude]
     [JsonPropertyName(nameof(ConfirmPassword))]
     public string? ConfirmPassword { get; set; }
}