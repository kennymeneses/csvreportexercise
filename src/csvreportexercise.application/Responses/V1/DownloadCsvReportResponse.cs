namespace csvreportexercise.application.Responses.V1;

public class DownloadCsvReportResponse
{
    public string FileName { get; set; } = null!;

    public string ContentType { get; set; } = null!;

    public byte[] Bytes { get; set; } = Array.Empty<byte>();
}