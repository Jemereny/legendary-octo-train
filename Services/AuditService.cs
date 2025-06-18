using SingularHealth.Mappers;
using SingularHealth.Models.Domain;
using SingularHealth.Models.Response;

namespace SingularHealth.Services;

public class AuditService(AuditPersistence auditPersistence)
{
  public IEnumerable<AuditEventResponse> GetAudit()
  {
    return auditPersistence.GetAudits().Select(AuditMapper.ToEventModel);
  }

  public IEnumerable<AuditEventResponse> GetAuditsForReplay(string[] ids)
  {
    var audits = auditPersistence.GetAudits(ids);

    // Sort to replay events in correct time order
    audits.ToList().Sort((a, b) => a.Timestamp.CompareTo(b.Timestamp));

    return audits.Select(AuditMapper.ToEventModel);
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
