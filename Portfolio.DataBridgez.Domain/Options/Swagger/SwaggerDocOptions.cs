using System.Text.Json.Serialization;

namespace Portfolio.DataBridgez.Domain.Options.Swagger;

public sealed class SwaggerDocOptions
{
    [JsonInclude]
    [JsonPropertyName(nameof(Name))]
    public string Name { get; set; }  = default!;
    
    [JsonInclude]
    [JsonPropertyName(nameof(OpenApiInfo))]
    public OpenApiInfoOptions OpenApiInfo { get; set; }  = default!;
}