
using SingularHealth.Models.Persistence;
using SingularHealth.Models.Domain;

namespace SingularHealth.Services;

public class AuditPersistence
{
  // Currently not thread safe - probably need create a concurrent list using locking
  // Should work for now
  private List<PersistentAudit> audits;

  public IEnumerable<Audit> GetAudits(Audit audit)
  {
    throw new NotImplementedException();
  }

  public Audit AddAudit(Audit audit)
  {
    throw new NotImplementedException(); ;

    return audit;
  }
}
