using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json;
using static Portfolio.DataBridgez.Domain.Constants.AppDbDefaults;
using static Portfolio.DataBridgez.Domain.Constants.AppDbDefaults.Tables.Entity;

namespace Portfolio.DataBridgez.Domain.Entities;

[DataContract(Name = nameof(Entity))]
public class Entity
{
    [Column(PrimaryKeyColumnName)]
    [Key]
    [Required(ErrorMessage = RequireMessageTemplate)]
    [DisplayName(PrimaryKeyDisplayName)]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [DataMember(Name = nameof(Id))]
    public long Id { get; set; }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}