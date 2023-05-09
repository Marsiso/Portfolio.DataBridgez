using System.Runtime.Serialization;
using System.Text.Json;
using FluentValidation;
using FluentValidation.Results;

namespace Portfolio.DataBridgez.Domain.Dtos.Get;

[DataContract(Name = "ValidationFailure")]
public sealed class ValidationFailureResponse
{
    [DataMember(Name = nameof(PropertyName))]
    public string PropertyName { get; set; } = default!;
    
    [DataMember(Name = nameof(ErrorMessage))]
    public string ErrorMessage { get; set; } = default!;
    
    [DataMember(Name = nameof(AttemptedValue))]
    public object AttemptedValue { get; set; } = default!;

    [DataMember(Name = nameof(Severity))]
    public Severity Severity { get; set; } = Severity.Error;
    
    [DataMember(Name = nameof(ErrorCode))]
    public string ErrorCode { get; set; } = default!;

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}