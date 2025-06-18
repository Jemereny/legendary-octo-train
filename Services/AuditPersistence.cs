
using SingularHealth.Models.Persistence;
using SingularHealth.Models.Domain;
using SingularHealth.Mappers;

namespace SingularHealth.Services;

public class AuditPersistence
{
  // Currently not thread safe - probably need create a concurrent list using locking
  // Should work for now
  private List<PersistentAudit> audits = new();

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
