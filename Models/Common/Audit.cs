namespace SingularHealth.Models.Common;

/// <summary>
/// This class is used to store common fields between domain and persistence
/// </summary>
public abstract class CommonAudit
{
  public required string Id { get; init; }

  public required DateTime Timestamp { get; init; }

  public required string ServiceName { get; init; }

  public required string EventType { get; init; }

  // Assuming this is the payload that performed the action, in string. In either Base64 or JSON.
  // Base64 is possible as it can encode binary data
  public required string Payload { get; init; }
}
