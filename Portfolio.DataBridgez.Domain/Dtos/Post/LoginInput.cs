using System.Runtime.Serialization;
using Portfolio.DataBridgez.Domain.Entities;

namespace Portfolio.Databridgez.Domain.Dtos.Post;

[DataContract(Name = nameof(LoginInput))]
public sealed class LoginInput
{
    /// <summary>
    ///     Either the <see cref="AppUser.UserName" /> or the <see cref="AppUser.Email" />
    ///     as log in credential option.
    /// </summary>
    [DataMember(Name = nameof(UserName))]
    public string UserName { get; set; } = default!;

    /// <summary>
    ///     The password credential to be verified.
    /// </summary>
    [DataMember(Name = nameof(Password))]
    public string Password { get; set; } = default!;
}