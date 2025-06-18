using SingularHealth.Models.Domain;

namespace SingularHealth.Services;

class AuditService(AuditPersistence auditPersistence)
{
  public Audit GetAudit(string id)
  {
    throw new NotImplementedException();
  }

  public Audit AddAudit(Audit audit)
  {
    auditPersistence.AddAudit(audit);

    return audit;
  }
}
