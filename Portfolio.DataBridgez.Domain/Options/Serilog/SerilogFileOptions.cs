using System.Text.Json.Serialization;

namespace Portfolio.DataBridgez.Domain.Options.Serilog;

public sealed class SerilogFileOptions
{
    [JsonInclude]
    [JsonPropertyName(nameof(OutputTemplate))]
    public string OutputTemplate { get; set; }  = default!;

    [JsonInclude]
    [JsonPropertyName(nameof(Path))]
    public string Path { get; set; }  = default!;
}