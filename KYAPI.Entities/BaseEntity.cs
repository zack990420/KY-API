using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace KYAPI.Entities;

public abstract class BaseEntity
{
    public long Id { get; set; }

    public long EntryBy { get; set; }

    [JsonIgnore]
    [ForeignKey(nameof(EntryBy))]
    public virtual ApplicationUser? User { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public long UpdatedBy { get; set; }

    [JsonIgnore]
    [ForeignKey(nameof(UpdatedBy))]
    public virtual ApplicationUser? UpdatedUser { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
