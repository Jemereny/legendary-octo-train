using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using SingularHealth.Models.Events;
using SingularHealth.Services;

namespace SingularHealth.Controllers;

[ApiController]
public class AuditController(ILogger<AuditController> logger, AuditService auditService) : ControllerBase
{
  ILogger<AuditController> _logger = logger;

  [HttpGet("events")]
  [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AuditEvent))]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  public IActionResult GetEvents()
  {
    return Ok(auditService.GetAudit());
  }

  [HttpPost("events")]
  [ProducesResponseType(StatusCodes.Status201Created)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  public IActionResult PostEvents([FromBody] AuditEvent auditEvent)
  {
    var audit = auditService.AddAudit(
      auditEvent.TimestampInISO8601,
      auditEvent.ServiceName,
      auditEvent.EventType,
      auditEvent.Payload);

    return Ok(new { id = audit.Id });
  }

  [HttpPost("events/replay")]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  public IActionResult ReplayEvents([FromBody] ReplayEvent replayEvent)
  {
    var selectedAudit = auditService.GetAuditsForReplay(replayEvent.Ids);

    _logger.LogInformation($"Replaying selected events: {JsonSerializer.Serialize(selectedAudit.Select(audit => audit.Id))}"); ;

    return NoContent();
  }
}
