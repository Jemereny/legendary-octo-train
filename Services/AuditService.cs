using SingularHealth.Models.Domain;

namespace SingularHealth.Services;

public class AuditService(AuditPersistence auditPersistence)
{
  public Audit GetAudit(string id)
  {
    throw new NotImplementedException();
  }

  public Audit AddAudit(string timestampInISO8601, string serviceName, string eventType, string payload)
  {
    var audit = new Audit
    {
      Id = Guid.NewGuid().ToString(),
      Timestamp = DateTimeOffset.Parse(timestampInISO8601).UtcDateTime,
      ServiceName = serviceName,
      EventType = eventType,
      Payload = payload
    };

    return auditPersistence.AddAudit(audit);
  }
}
