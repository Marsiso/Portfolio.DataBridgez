using System.Text.Json.Serialization;

namespace Portfolio.Databridgez.Domain.Options.Serilog;

public sealed class SerilogSeqOptions
{
    [JsonInclude]
    [JsonPropertyName(nameof(ServerUrl))]
    public string ServerUrl { get; set; }
}