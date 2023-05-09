using System.Runtime.Serialization;
using System.Text.Json;
using Portfolio.DataBridgez.Domain.Entities;

namespace Portfolio.Databridgez.Domain.Dtos.Post;

[DataContract(Name = nameof(RegisterInput))]
public sealed class RegisterInput
{
     /// <summary>
     ///     The <see cref="AppUser.UserName" /> credential.
     /// </summary>
     [DataMember(Name = nameof(UserName))]
    public string? UserName { get; set; }

     /// <summary>
     ///     The <see cref="AppUser.Email" /> credential.
     /// </summary>
     [DataMember(Name = nameof(Email))]
    public string? Email { get; set; }

     /// <summary>
     ///     The password credential.
     /// </summary>
     [DataMember(Name = nameof(Password))]
    public string? Password { get; set; }

     /// <summary>
     ///     The confirmation password credential.
     /// </summary>
     [DataMember(Name = nameof(ConfirmPassword))]
    public string? ConfirmPassword { get; set; }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}