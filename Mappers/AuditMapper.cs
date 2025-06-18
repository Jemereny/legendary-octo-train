using SingularHealth.Models.Persistence;
using SingularHealth.Models.Domain;
using SingularHealth.Models.Events;

namespace SingularHealth.Mappers;

static class AuditMapper
{
  public static Audit ToDomainModel(PersistentAudit audit)
  {
    return new Audit
    {
      Id = audit.Id,
      Timestamp = audit.Timestamp,
      ServiceName = audit.ServiceName,
      EventType = audit.EventType,
      Payload = audit.Payload
    };
    throw new NotImplementedException();
  }

  public static PersistentAudit ToPersistenceModel(Audit audit)
  {
    return new PersistentAudit
    {
      Id = audit.Id,
      Timestamp = audit.Timestamp,
      ServiceName = audit.ServiceName,
      EventType = audit.EventType,
      Payload = audit.Payload
    };
  }
}
