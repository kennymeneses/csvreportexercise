using csvreportexercise.application.Requests.V1;
using csvreportexercise.application.Responses.V1;

namespace csvreportexercise.application.Handlers.V1.Interfaces;

public interface ICsvReportHandler
{
    Task<DownloadCsvReportResponse> GetCsvReport(IsbnInfoFilteredRequest request, CancellationToken cancellationToken);
}