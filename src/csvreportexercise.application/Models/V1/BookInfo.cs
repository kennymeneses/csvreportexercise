using csvreportexercise.application.Commons.V1.Enums;

namespace csvreportexercise.application.Models.V1;

public sealed class BookInfo
{
    public int RowNumber { get; set; }
    public DataRetrievalType Type { get; set; }
    public string Isbn { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Subtitle { get; set; } = string.Empty;
    public string AuthorName { get; set; } = string.Empty;
    public int NumberPages { get; set; }
    public string PublishDate { get; set; } = string.Empty;
}