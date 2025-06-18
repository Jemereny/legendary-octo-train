
using SingularHealth.Models.Persistence;
using SingularHealth.Models.Domain;
using SingularHealth.Mappers;
using System.Collections.Concurrent;

namespace SingularHealth.Services;

public class AuditPersistence
{
  private ConcurrentBag<PersistentAudit> audits = new();

  public IEnumerable<Audit> GetAudits()
  {
    return audits.Select(AuditMapper.ToDomainModel);
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
