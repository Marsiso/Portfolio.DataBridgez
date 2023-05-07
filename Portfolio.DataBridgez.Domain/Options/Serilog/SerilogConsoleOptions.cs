using System.Text.Json.Serialization;

namespace Portfolio.DataBridgez.Domain.Options.Serilog;

public sealed class SerilogConsoleOptions
{
    [JsonInclude]
    [JsonPropertyName(nameof(OutputTemplate))]
    public string OutputTemplate { get; set; } = default!;
}