using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;
using static Portfolio.DataBridgez.Domain.Constants.AppDbDefaults;
using static Portfolio.DataBridgez.Domain.Constants.AppDbDefaults.Tables.Entity;

namespace Portfolio.DataBridgez.Domain.Entities;

public class Entity
{
    [Column(PrimaryKeyColumnName)]
    [Key]
    [Required(ErrorMessage = RequireMessageTemplate)]
    [DisplayName(PrimaryKeyDisplayName)]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [JsonIgnore]
    [JsonPropertyName(nameof(Id))]
    public long Id { get; set; }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}