using System.Text.Json.Serialization;

namespace Portfolio.Databridgez.Domain.Options.Swagger;

public sealed class SwaggerDocOptions
{
    [JsonInclude]
    [JsonPropertyName(nameof(Name))]
    public string Name { get; set; }
    
    [JsonInclude]
    [JsonPropertyName(nameof(OpenApiInfo))]
    public OpenApiInfoOptions OpenApiInfo { get; set; }
}