using System.Net;
using System.Text.Json;
using csvreportexercise.application.Models.V1;
using csvreportexercise.application.Services.V1.Interfaces;
using Microsoft.Extensions.Options;

namespace csvreportexercise.application.Services.V1;

public class OpenLibraryService(HttpClient httpClient, IOptions<OpenLibrarySettings> openLibrarySection) : IOpenLibraryService
{
    public async Task<OpenLibraryBookInfo?> GetBookInfo(string isbn)
    {
        string uri = openLibrarySection.Value.BooksInfoUri;
        
        using HttpResponseMessage response = await httpClient.GetAsync($"{uri}{isbn}");

        if (response.StatusCode != HttpStatusCode.OK)
        {
            throw new ApplicationException();
        }
        
        var jsonResponse = await response.Content.ReadAsStringAsync();
        
        var jsonDocument = JsonDocument.Parse(jsonResponse);
        
        var bookInfoJson = jsonDocument.RootElement.GetProperty($"ISBN:{isbn}").GetRawText();

        OpenLibraryBookInfo? bookInfo = JsonSerializer.Deserialize<OpenLibraryBookInfo>(bookInfoJson);

        return bookInfo;
    }
}