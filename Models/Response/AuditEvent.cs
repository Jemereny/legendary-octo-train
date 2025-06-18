using System.ComponentModel.DataAnnotations;

namespace SingularHealth.Models.Response;

public class AuditEventResponse
{
  public required string Id { get; init; }
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
