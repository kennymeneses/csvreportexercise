using csvreportexercise.application.Commons.V1.Enums;
using csvreportexercise.application.Handlers.V1;
using csvreportexercise.application.Models.V1;
using csvreportexercise.application.Requests.V1;
using csvreportexercise.application.Responses.V1;
using csvreportexercise.application.Services.V1.Interfaces;
using FluentAssertions;
using NSubstitute;

namespace csvreportexercise.test;

public class CsvReportHandlerTests
{
    private IOpenLibraryService openLibraryService;
    private ICacheService cacheService;
    private CsvReportHandler sust;

    public CsvReportHandlerTests()
    {
        openLibraryService = Substitute.For<IOpenLibraryService>();
        cacheService = Substitute.For<ICacheService>();

        sust = new CsvReportHandler(openLibraryService, cacheService);
    }

    [Fact]
    public async Task WhenRequestIsValidCsvHandlerShouldReturnOkResponse()
    {
        OpenLibraryBookInfo fullBookInfo = new()
        {
            Details = new Details()
            {
                Title = "RandoTitle",
                Subtitle = "RandomSubtitule",
                NumberOfPages = 500,
                PublishDate = "May 2024",
                Authors = new List<Author>{new Author{Key = Guid.NewGuid().ToString(), Name = "Bocaccio"}}
            }
        };
        openLibraryService.GetBookInfo("isbnValue").Returns(fullBookInfo);
        BookInfo bookInfo = new BookInfo()
        {
            RowNumber = 1,
            Isbn = "0201558025",
            Type = DataRetrievalType.Server,
            Title = fullBookInfo.Details.Title,
            Subtitle = fullBookInfo.Details.Subtitle,
            AuthorName = fullBookInfo.Details.Authors[0].Name,
            NumberPages = fullBookInfo.Details.NumberOfPages,
            PublishDate = fullBookInfo.Details.PublishDate
        };
        
        cacheService.StoreBookInfo(bookInfo);

        var dictIsbn = new Dictionary<string, int>();
        dictIsbn.Add("0201558025", 1);
        
        var filteredRequest = new IsbnInfoFilteredRequest()
        {
            IsbnDict = dictIsbn,
            BooksStored = new List<BookInfo>() {bookInfo}
        };

        DownloadCsvReportResponse response = new();

        var result = await sust.GetCsvReport(filteredRequest, CancellationToken.None);

        result.Should().NotBeNull();
        result.FileName.Should().NotBeEmpty();
        result.FileName.Should().Be("FullOpenLibraryReport.xlsx");
        result.Bytes.Should().NotBeEmpty();
    }
}