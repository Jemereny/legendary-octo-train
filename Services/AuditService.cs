using SingularHealth.Mappers;
using SingularHealth.Models.Domain;
using SingularHealth.Models.Response;

namespace SingularHealth.Services;

public class AuditService(AuditPersistence auditPersistence)
{
  public IEnumerable<AuditEventResponse> GetAudits(string? serviceName, string? eventType, Tuple<DateTime, DateTime>? timeRange)
  {
    var audits = auditPersistence.GetAudits(serviceName, eventType, timeRange);

    return audits.OrderBy(audit => audit.Timestamp).Select(AuditMapper.ToEventModel);
  }

  public IEnumerable<AuditEventResponse> GetAuditsForReplay(string[] ids)
  {
    var audits = auditPersistence.GetAudits(ids);

    // Sort to replay events in the ascending time order
    return audits.OrderBy(audit => audit.Timestamp).Select(AuditMapper.ToEventModel);
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
