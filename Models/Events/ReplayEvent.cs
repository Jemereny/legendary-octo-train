using System.ComponentModel.DataAnnotations;

namespace SingularHealth.Models.Events;

public class ReplayEvent
{
  [Required]
  public required string[] Ids { get; init; }
}
