using System.Runtime.Serialization;
using System.Text.Json;

namespace Portfolio.Databridgez.Domain.Dtos.Post;

[DataContract(Name = nameof(RegisterInput))]
public sealed class RegisterInput
{
     [DataMember(Name = nameof(UserName))]
     public string? UserName { get; set; }
     
     [DataMember(Name = nameof(Email))]
     public string? Email { get; set; }
     
     [DataMember(Name = nameof(Password))]
     public string? Password { get; set; }
     
     [DataMember(Name = nameof(ConfirmPassword))]
     public string? ConfirmPassword { get; set; }

     public override string ToString()
     {
          return JsonSerializer.Serialize(this);
     }
}