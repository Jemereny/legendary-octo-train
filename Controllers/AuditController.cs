using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using SingularHealth.Models.Events;

namespace SingularHealth.Controllers;

[ApiController]
public class AuditController(ILogger<AuditController> logger) : ControllerBase
{
  ILogger<AuditController> _logger = logger;

  [HttpGet("events")]
  [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AuditEvent))]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  public async Task<IActionResult> GetEvents()
  {
    return Ok(Array.Empty<object>());
  }

  [HttpPost("events")]
  [ProducesResponseType(StatusCodes.Status201Created)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  public IEnumerable<string> PostEvents([FromBody] AuditEvent auditEvent)
  {
    Console.WriteLine("hello world");
    Console.WriteLine(JsonSerializer.Serialize(auditEvent));
    return [];
  }

  [HttpPost("events/replay")]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  public void ReplayEvents()
  {
    return;
  }
}
