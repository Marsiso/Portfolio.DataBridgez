using System.Text.Json.Serialization;

namespace Portfolio.Databridgez.Domain.Options.Swagger;

public sealed class OpenApiInfoContactOptions
{
    [JsonInclude]
    [JsonPropertyName(nameof(Email))]
    public string Email { get; set; }
    
    [JsonInclude]
    [JsonPropertyName(nameof(Name))]
    public string Name { get; set; }
    
    [JsonInclude]
    [JsonPropertyName(nameof(Url))]
    public string Url { get; set; }
}