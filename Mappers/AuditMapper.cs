using SingularHealth.Models.Persistence;
using SingularHealth.Models.Domain;
using SingularHealth.Models.Response;
using System.Globalization;

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
  }

  public static AuditEventResponse ToEventModel(Audit audit)
  {
    return new AuditEventResponse
    {
      Id = audit.Id,
      TimestampInISO8601 = audit.Timestamp.ToString("o", CultureInfo.InvariantCulture),
      ServiceName = audit.ServiceName,
      EventType = audit.EventType,
      Payload = audit.Payload
    };
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
