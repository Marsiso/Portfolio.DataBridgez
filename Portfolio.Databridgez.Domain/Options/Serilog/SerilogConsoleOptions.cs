using System.Text.Json.Serialization;

namespace Portfolio.Databridgez.Domain.Options.Serilog;

public sealed class SerilogConsoleOptions
{
    [JsonInclude]
    [JsonPropertyName(nameof(OutputTemplate))]
    public string OutputTemplate { get; set; }
}