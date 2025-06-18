using System.ComponentModel.DataAnnotations;

namespace SingularHealth.Models.Events;

public class AuditEvent
{
  /// <summary>
  /// Timestamp in ISO8601
  /// </summary>
  [Required]
  public required string TimestampInISO8601 { get; init; }

  [Required]
  public required string ServiceName { get; init; }

  [Required]
  public required string EventType { get; init; }

  [Required]
  public required string Payload { get; init; }
}
