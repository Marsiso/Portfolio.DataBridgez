using System.Text.Json.Serialization;

namespace Portfolio.Databridgez.Domain.Options.Serilog;

public sealed class SerilogSinkOptions
{
    [JsonInclude]
    [JsonPropertyName(nameof(Console))]
    public SerilogConsoleOptions Console { get; set; }
    
    [JsonInclude]
    [JsonPropertyName(nameof(File))]
    public SerilogFileOptions File { get; set; }
    
    [JsonInclude]
    [JsonPropertyName(nameof(Seq))]
    public SerilogSeqOptions Seq { get; set; }
}