using System.Text.Json.Serialization;

namespace Portfolio.Databridgez.Domain.Options.Swagger;

public sealed class SwaggerOptions
{
    [JsonInclude]
    [JsonPropertyName(nameof(RouteTemplate))]
    public string RouteTemplate { get; set; }
    
    [JsonInclude]
    [JsonPropertyName(nameof(Description))]
    public string Description { get; set; }
    
    [JsonInclude]
    [JsonPropertyName(nameof(UiEndpoint))]
    public string UiEndpoint { get; set; }
    
    [JsonInclude]
    [JsonPropertyName(nameof(Doc))]
    public SwaggerDocOptions Doc { get; set; }
}