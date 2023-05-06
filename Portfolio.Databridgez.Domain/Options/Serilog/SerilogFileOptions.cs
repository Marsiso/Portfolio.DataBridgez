using System.Text.Json.Serialization;

namespace Portfolio.Databridgez.Domain.Options.Serilog;

public sealed class SerilogFileOptions
{
    [JsonInclude]
    [JsonPropertyName(nameof(OutputTemplate))]
    public string OutputTemplate { get; set; }

    [JsonInclude]
    [JsonPropertyName(nameof(Path))]
    public string Path { get; set; }
}