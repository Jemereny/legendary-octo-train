
using SingularHealth.Models.Persistence;
using SingularHealth.Models.Domain;
using SingularHealth.Mappers;

namespace SingularHealth.Services;

public class AuditPersistence
{
  // Currently not thread safe - probably need create a concurrent list using locking
  // Should work for now
  private List<PersistentAudit> audits = new();

  public IEnumerable<Audit> GetAudits(Audit audit)
  {
    throw new NotImplementedException();
  }

  public Audit AddAudit(Audit audit)
  {
    audits.Add(AuditMapper.ToPersistenceModel(audit));

    return audit;
  }
}
