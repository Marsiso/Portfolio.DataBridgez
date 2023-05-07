using System.Text.Json.Serialization;

namespace Portfolio.DataBridgez.Domain.Options.Serilog;

public sealed class SerilogOptions
{
    [JsonInclude]
    [JsonPropertyName(nameof(Console))]
    public SerilogConsoleOptions Console { get; set; }  = default!;
    
    [JsonInclude]
    [JsonPropertyName(nameof(File))]
    public SerilogFileOptions File { get; set; }  = default!;
    
    [JsonInclude]
    [JsonPropertyName(nameof(Seq))]
    public SerilogSeqOptions Seq { get; set; }  = default!;
}