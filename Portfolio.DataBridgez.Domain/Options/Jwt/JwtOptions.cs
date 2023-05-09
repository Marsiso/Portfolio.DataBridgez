using System.Runtime.Serialization;

namespace Portfolio.Databridgez.Domain.Options.Jwt;

[DataContract(Name = nameof(JwtOptions))]
public sealed class JwtOptions
{
    [DataMember(Name = nameof(ValidAudience))]
    public string ValidAudience { get; set; } = default!;

    [DataMember(Name = nameof(ValidIssuer))]
    public string ValidIssuer { get; set; } = default!;
    
    [DataMember(Name = nameof(Secret))]
    public string Secret { get; set; } = default!;
}