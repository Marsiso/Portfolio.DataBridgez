using System.Text.Json.Serialization;

namespace Portfolio.Databridgez.Domain.Options.Swagger;

public class OpenApiInfoOptions
{
    [JsonInclude]
    [JsonPropertyName(nameof(Title))]
    public string Title { get; set; }

    [JsonInclude]
    [JsonPropertyName(nameof(Version))]
    public string Version { get; set; }

    [JsonInclude]
    [JsonPropertyName(nameof(Description))]
    public string Description { get; set; }

    [JsonInclude]
    [JsonPropertyName(nameof(Contact))]
    public OpenApiInfoContactOptions Contact { get; set; }
}