using System.Runtime.Serialization;
using System.Text.Json;

namespace Portfolio.DataBridgez.Domain.Dtos.Get;

[DataContract(Name = nameof(GlobalExceptionResponse))]
public sealed class GlobalExceptionResponse
{
    /// <summary>
    ///     Exception details.
    /// </summary>
    [DataMember(Name = nameof(Message))]
    public string Message { get; set; }

    /// <summary>
    ///     HTTP status code.
    /// </summary>
    [DataMember(Name = nameof(StatusCode))]
    public int StatusCode { get; set; }

    public GlobalExceptionResponse(string message, int statusCode)
    {
        Message = message;
        StatusCode = statusCode;
    }

    public override string ToString() => JsonSerializer.Serialize(this);
}