using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using static Portfolio.DataBridgez.Domain.Constants.AppDbDefaults;
using static Portfolio.DataBridgez.Domain.Constants.AppDbDefaults.Tables.AppUsers;
using static Portfolio.DataBridgez.Domain.Constants.AppDbDefaults.Tables.AppUsers.Column;

namespace Portfolio.DataBridgez.Domain.Entities;

[Table(TableName, Schema = SchemaName)]
[Index(nameof(NormalizedUserName), IsUnique = true)]
[Index(nameof(NormalizedEmail), IsUnique = true)]
public sealed class AppUser : Entity
{
    public AppUser()
    {
    }

    public AppUser(string userName)
    {
        UserName = userName;
    }

    [Column(UserNameColumnName)]
    [Unicode]
    [Required(ErrorMessage = RequireMessageTemplate)]
    [StringLength(MaximumUserNameLengthConstraint, MinimumLength = MinimumUserNameLengthConstraint,
        ErrorMessage = StringLengthMessageTemplate)]
    [DisplayName(UserNameDisplayName)]
    [DataType(DataType.Text)]
    [JsonInclude]
    [JsonPropertyName(nameof(UserName))]
    public string UserName { get; set; } = default!;

    [Column(NormalizedUserNameColumnName)]
    [Unicode]
    [Required(ErrorMessage = RequireMessageTemplate)]
    [StringLength(MaximumUserNameLengthConstraint, MinimumLength = MinimumUserNameLengthConstraint,
        ErrorMessage = StringLengthMessageTemplate)]
    [DisplayName(NormalizedUserNameDisplayName)]
    [DataType(DataType.Text)]
    [JsonInclude]
    [JsonPropertyName(nameof(NormalizedUserName))]
    public string NormalizedUserName { get; set; } = default!;

    [Column(EmailColumnName)]
    [Unicode]
    [Required(ErrorMessage = RequireMessageTemplate)]
    [StringLength(MaximumEmailLengthConstraint,
        ErrorMessage = MaximumStringLengthMessageTemplate)]
    [DisplayName(EmailDisplayName)]
    [DataType(DataType.EmailAddress)]
    [JsonInclude]
    [JsonPropertyName(nameof(Email))]
    public string Email { get; set; } = default!;

    [Column(NormalizedEmailColumnName)]
    [Unicode]
    [Required(ErrorMessage = RequireMessageTemplate)]
    [StringLength(MaximumEmailLengthConstraint,
        ErrorMessage = MaximumStringLengthMessageTemplate)]
    [DisplayName(NormalizedEmailDisplayName)]
    [DataType(DataType.EmailAddress)]
    [JsonInclude]
    [JsonPropertyName(nameof(NormalizedEmail))]
    public string NormalizedEmail { get; set; }  = default!;

    [Column(EmailConfirmedColumnName)]
    [Required(ErrorMessage = RequireMessageTemplate)]
    [DisplayName(EmailConfirmedDisplayName)]
    [JsonInclude]
    [JsonPropertyName(nameof(EmailConfirmed))]
    public bool EmailConfirmed { get; set; }

    [Column(PasswordHashColumnName)]
    [Required(ErrorMessage = RequireMessageTemplate)]
    [Unicode]
    [DataType(DataType.Password)]
    [DisplayName(PasswordHashDisplayName)]
    [JsonIgnore]
    public string PasswordHash { get; set; } = default!;

    [Column(SecurityStampColumnName)]
    [Required(ErrorMessage = RequireMessageTemplate)]
    [Unicode]
    [DataType(DataType.Text)]
    [DisplayName(SecurityStampDisplayName)]
    [JsonIgnore]
    public string SecurityStamp { get; set; }  = default!;

    [Column(ConcurrencyStampColumnName)]
    [DisplayName(ConcurrencyStampDisplayName)]
    [Required(ErrorMessage = RequireMessageTemplate)]
    [ConcurrencyCheck]
    [Timestamp]
    [JsonIgnore]
    public byte[] ConcurrencyStamp { get; set; }  = default!;

    [Column(LockoutEndColumnName)]
    [DisplayName(LockoutEndDisplayName)]
    [DataType(DataType.DateTime)]
    [JsonInclude]
    [JsonPropertyName(nameof(LockoutEnd))]
    public DateTimeOffset? LockoutEnd { get; set; }

    [Column(LockoutEnabledColumnName)]
    [DisplayName(LockoutEnabledDisplayName)]
    [Required(ErrorMessage = RequireMessageTemplate)]
    [JsonInclude]
    [JsonPropertyName(nameof(LockoutEnabled))]
    public bool LockoutEnabled { get; set; } = true;

    [Column(AccessFailedCountColumnName)]
    [DisplayName(AccessFailedCountDisplayName)]
    [Required(ErrorMessage = RequireMessageTemplate)]
    [JsonInclude]
    [JsonPropertyName(nameof(AccessFailedCount))]
    public int AccessFailedCount { get; set; }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}