
using SingularHealth.Models.Persistence;
using SingularHealth.Models.Domain;
using SingularHealth.Mappers;
using System.Collections.Concurrent;

namespace SingularHealth.Services;

public class AuditPersistence
{
  private ConcurrentBag<PersistentAudit> audits = new();

  public IEnumerable<Audit> GetAudits(string? serviceName, string? eventType, Tuple<DateTime, DateTime>? timeRange)
  {
    var currentAudits = audits.Select(x => x);

    if (serviceName != null) currentAudits = currentAudits.Where(audit => audit.ServiceName == serviceName);
    if (eventType != null) currentAudits = currentAudits.Where(audit => audit.EventType == eventType);
    if (timeRange != null) currentAudits = currentAudits.Where(audit => timeRange.Item1.CompareTo(audit.Timestamp) <= 0 && timeRange.Item2.CompareTo(audit.Timestamp) >= 0);

    return currentAudits
    .Select(AuditMapper.ToDomainModel);
  }

  public IEnumerable<Audit> GetAudits(string[] ids)
  {
    var selectedIds = new HashSet<string>(ids);
    return audits
    .Where(audit => selectedIds.Contains(audit.Id))
    .Select(AuditMapper.ToDomainModel);
  }

  public Audit AddAudit(Audit audit)
  {
    audits.Add(AuditMapper.ToPersistenceModel(audit));

    return audit;
  }
}
