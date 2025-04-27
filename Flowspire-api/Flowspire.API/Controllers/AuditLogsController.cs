using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Flowspire.Application.Interfaces;
using Flowspire.API.Common;
using Flowspire.Application.Common;

namespace Flowspire.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Administrator")]
public class AuditLogsController(IAuditLogService auditLogService, ILogger<AuditLogsController> logger) : ControllerBase
{
    private readonly IAuditLogService _auditLogService = auditLogService;
    private readonly ILogger<AuditLogsController> _logger = logger;

    [HttpGet]
    public async Task<IActionResult> GetAuditLogs([FromQuery] PaginationQuery query)
        => await ControllerHelper.ExecuteAsync(async () =>
        {
            var pagedLogs = await _auditLogService.GetAuditLogsAsync(query);

            if (pagedLogs == null || !pagedLogs.Data.Any())
                throw new Exception(ErrorMessages.NoAuditLogsFound);

            return pagedLogs;
        }, _logger, this, SuccessMessages.AuditLogsRetrieved);
}
