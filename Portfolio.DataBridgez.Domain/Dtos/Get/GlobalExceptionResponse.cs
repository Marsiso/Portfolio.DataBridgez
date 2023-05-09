using System.Text.Json;
using System.Text.Json.Serialization;

namespace Portfolio.DataBridgez.Domain.Dtos.Get;

public sealed class GlobalExceptionResponse
{
    /// <summary>
    ///     Exception details.
    /// </summary>
    [JsonInclude]
    [JsonPropertyName(nameof(Message))]
    public string Message { get; set; }

    /// <summary>
    ///     HTTP status code.
    /// </summary>
    [JsonInclude]
    [JsonPropertyName(nameof(StatusCode))]
    public int StatusCode { get; set; }

    public GlobalExceptionResponse(string message, int statusCode)
    {
        Message = message;
        StatusCode = statusCode;
    }

    public override string ToString() => JsonSerializer.Serialize(this);
}