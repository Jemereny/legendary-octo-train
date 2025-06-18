using SingularHealth.Models.Persistence;
using SingularHealth.Models.Domain;
using SingularHealth.Models.Events;

namespace SingularHealth.Mappers;

static class AuditMapper
{
  public static Audit ToDomainModel(PersistentAudit audit)
  {
    throw new NotImplementedException();
  }

  public static Audit ToDomainModel(AuditEvent audit)
  {
    throw new NotImplementedException();
  }

  public static Audit ToPersistenceModel(PersistentAudit audit)
  {
    throw new NotImplementedException();
  }
}
