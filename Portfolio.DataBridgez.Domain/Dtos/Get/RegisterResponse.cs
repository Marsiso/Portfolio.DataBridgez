using System.Text.Json;
using System.Text.Json.Serialization;
using FluentValidation.Results;

namespace Portfolio.DataBridgez.Domain.Dtos.Get;

public sealed class RegisterResponse
{
    /// <summary>
    ///     HTTP status code.
    /// </summary>
    [JsonInclude]
    [JsonPropertyName(nameof(StatusCode))]
    public int StatusCode { get; set; }

    /// <summary>
    ///     Description.
    /// </summary>
    [JsonInclude]
    [JsonPropertyName(nameof(Message))]
    public string? Message { get; set; }

    /// <summary>
    ///     Collection of model validation errors.
    /// </summary>
    [JsonInclude]
    [JsonPropertyName(nameof(Errors))]
    public List<ValidationFailure>? Errors { get; set; }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}