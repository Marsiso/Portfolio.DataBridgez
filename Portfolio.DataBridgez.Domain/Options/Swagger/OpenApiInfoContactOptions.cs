using System.Text.Json.Serialization;

namespace Portfolio.DataBridgez.Domain.Options.Swagger;

public sealed class OpenApiInfoContactOptions
{
    [JsonInclude]
    [JsonPropertyName(nameof(Email))]
    public string Email { get; set; }  = default!;
    
    [JsonInclude]
    [JsonPropertyName(nameof(Name))]
    public string Name { get; set; }  = default!;
    
    [JsonInclude]
    [JsonPropertyName(nameof(Url))]
    public string Url { get; set; }  = default!;
}