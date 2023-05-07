using System.Text.Json.Serialization;

namespace Portfolio.DataBridgez.Domain.Options.Swagger;

public class OpenApiInfoOptions
{
    [JsonInclude]
    [JsonPropertyName(nameof(Title))]
    public string Title { get; set; }  = default!;

    [JsonInclude]
    [JsonPropertyName(nameof(Version))]
    public string Version { get; set; }  = default!;

    [JsonInclude]
    [JsonPropertyName(nameof(Description))]
    public string Description { get; set; }  = default!;

    [JsonInclude]
    [JsonPropertyName(nameof(Contact))]
    public OpenApiInfoContactOptions Contact { get; set; }  = default!;
}