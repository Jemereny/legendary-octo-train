using System.ComponentModel.DataAnnotations;

namespace SingularHealth.Models.Events;

public class AuditEvent
{
  [Required]
  public required string Id { get; init; }

  [Required]
  public required string Timestamp { get; init; }

  [Required]
  public required string ServiceName { get; init; }

  [Required]
  public required string EventType { get; init; }

  [Required]
  public required string Payload { get; init; }
}
