using System.Runtime.Serialization;
using System.Text.Json;

namespace Portfolio.DataBridgez.Domain.Dtos.Get;

[DataContract(Name = nameof(LoginResponse))]
public sealed class LoginResponse
{
    /// <summary>
    ///     HTTP status code.
    /// </summary>
    [DataMember(Name = nameof(StatusCode))]
    public int StatusCode { get; set; }

    /// <summary>
    ///     Description.
    /// </summary>
    [DataMember(Name = nameof(Message))]
    public string? Message { get; set; }
    
    /// <summary>
    ///     Json web token.
    /// </summary>
    [DataMember(Name = nameof(Token))]
    public string? Token { get; set; }

    /// <summary>
    ///     Collection of model validation errors.
    /// </summary>
    [DataMember(Name = nameof(ValidationFailures))]
    public List<ValidationFailureResponse>? ValidationFailures { get; set; }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}