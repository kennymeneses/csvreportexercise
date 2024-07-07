using csvreportexercise.application.Requests.V1;
using Microsoft.AspNetCore.Mvc;

namespace csvreportexercise.api.Controllers.V1;

public class ReportsController: ControllerBase
{
    [HttpGet("download", Name = nameof(DownloadDocument))]
    [Produces("application/octet-stream", Type = typeof(FileResult))]
    [ProducesResponseType(typeof(FileResult), StatusCodes.Status200OK)]
    [ProducesErrorResponseType(typeof(ProblemDetails))]
    public async Task<IActionResult> DownloadDocument([FromForm] IsbnInfoRequest request ,CancellationToken cancellationToken)
    {
        return Ok();
    }
}