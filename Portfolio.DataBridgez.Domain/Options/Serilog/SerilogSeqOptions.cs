using System.Text.Json.Serialization;

namespace Portfolio.DataBridgez.Domain.Options.Serilog;

public sealed class SerilogSeqOptions
{
    [JsonInclude]
    [JsonPropertyName(nameof(ServerUrl))]
    public string ServerUrl { get; set; }  = default!;
}