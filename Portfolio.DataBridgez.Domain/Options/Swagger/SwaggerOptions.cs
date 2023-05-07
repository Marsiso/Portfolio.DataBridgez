using System.Text.Json.Serialization;

namespace Portfolio.DataBridgez.Domain.Options.Swagger;

public sealed class SwaggerOptions
{
    [JsonInclude]
    [JsonPropertyName(nameof(RouteTemplate))]
    public string RouteTemplate { get; set; }  = default!;
    
    [JsonInclude]
    [JsonPropertyName(nameof(Description))]
    public string Description { get; set; }  = default!;
    
    [JsonInclude]
    [JsonPropertyName(nameof(UiEndpoint))]
    public string UiEndpoint { get; set; }  = default!;
    
    [JsonInclude]
    [JsonPropertyName(nameof(Doc))]
    public SwaggerDocOptions Doc { get; set; }  = default!;
}