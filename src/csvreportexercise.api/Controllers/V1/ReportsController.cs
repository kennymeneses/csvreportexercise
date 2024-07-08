using csvreportexercise.application.Handlers.V1.Interfaces;
using csvreportexercise.application.Requests.V1;
using csvreportexercise.application.Services.V1.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace csvreportexercise.api.Controllers.V1;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class ReportsController(IFormFileService formFileService, ICacheService cache,  ICsvReportHandler handler): ControllerBase
{
    /// <summary>
    /// Generates an Excel file containing a full report of a list of books from ISBN resources.
    /// </summary>
    [HttpPost("download", Name = nameof(DownloadDocument))]
    [Produces("application/octet-stream", Type = typeof(FileResult))]
    [ProducesResponseType(typeof(FileResult), StatusCodes.Status200OK)]
    [ProducesErrorResponseType(typeof(ProblemDetails))]
    public async Task<IActionResult> DownloadDocument([FromForm] IsbnInfoRequest request ,CancellationToken cancellationToken)
    {
        var isbnDictionary = await formFileService.GetIsbnListAsync(request.File);
        var booksInMemory = cache.GetBookListStored(isbnDictionary.Keys.ToList());
        
        var filteredRequest = new IsbnInfoFilteredRequest()
        {
            IsbnDict = isbnDictionary.Where(pair => !booksInMemory.Any(book => book.Isbn == pair.Key)).ToDictionary(),
            BooksStored = booksInMemory
        };

        var result = await handler.GetCsvReport(filteredRequest, cancellationToken);

        return File(result.Bytes, result.ContentType, result.FileName);
    }
}