using System.Text.Json;
using System.Web;
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
  public IActionResult GetEvents([FromQuery] string? serviceName, [FromQuery] string? eventType, [FromQuery] string? timeRange)
  {
    _logger.LogInformation($"GET event for serviceName: {serviceName}, eventType: {eventType}, timeRange: {timeRange}");
    // Parse time range if it is passed in
    if (timeRange != null)
    {
      var decodedTimeRange = HttpUtility.UrlDecode(timeRange);
      try
      {
        var timeRangeTuple = ParseTimeRange(decodedTimeRange);
        if (timeRangeTuple == null) return BadRequest("Time range must be in an array format with ISO8601 timestamps where the earlier time range is the first item. E.g. [\"2025-06-18T00:00:00Z\", \"2025-06-19T00:00:0Z\"]");

        var timeRangeAudits = auditService.GetAudits(serviceName, eventType, timeRangeTuple);

        _logger.LogInformation($"Found {timeRangeAudits.Count()} records serviceName: {serviceName}, eventType: {eventType}, timeRange: {timeRange}");
        return Ok(timeRangeAudits);
      }
      catch (Exception)
      {
        return BadRequest("Time range must be in an array format with ISO8601 timestamps where the earlier time range is the first item. E.g. [\"2025-06-18T00:00:00Z\", \"2025-06-19T00:00:0Z\"]");
      }
    }

    var audits = auditService.GetAudits(serviceName, eventType, null);
    _logger.LogInformation($"Found {audits.Count()} records serviceName: {serviceName}, eventType: {eventType}, timeRange: {timeRange}");
    return Ok(audits);
  }

  [HttpPost("events")]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  public IActionResult PostEvents([FromBody] AuditEvent auditEvent)
  {
    _logger.LogInformation($"POST event for audit event {JsonSerializer.Serialize(auditEvent)}");
    var audit = auditService.AddAudit(
      auditEvent.TimestampInISO8601,
      auditEvent.ServiceName,
      auditEvent.EventType,
      auditEvent.Payload);

    _logger.LogInformation($"Created audit event id: {audit.Id}");
    return Ok(new { id = audit.Id });
  }

  [HttpPost("events/replay")]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  public IActionResult ReplayEvents([FromBody] ReplayEvent replayEvent)
  {
    _logger.LogInformation($"POST replay event for ids {JsonSerializer.Serialize(replayEvent.Ids)}");
    var selectedAudit = auditService.GetAuditsForReplay(replayEvent.Ids);

    _logger.LogInformation($"Replaying selected events: {JsonSerializer.Serialize(selectedAudit.Select(audit => audit.Id))}"); ;

    return NoContent();
  }

  private Tuple<DateTime, DateTime>? ParseTimeRange(string timeRangeInStr)
  {
    var deserializedTimeRange = JsonSerializer.Deserialize<string[]>(timeRangeInStr);

    if (deserializedTimeRange?.Length != 2) return null;

    var parsedEarlierTime = DateTimeOffset.Parse(deserializedTimeRange[0]).UtcDateTime;
    var parsedLaterTime = DateTimeOffset.Parse(deserializedTimeRange[1]).UtcDateTime;

    if (parsedEarlierTime.CompareTo(parsedLaterTime) > 0) return null;

    return Tuple.Create(
        parsedEarlierTime,
        parsedLaterTime);
  }
}
