using System.Text.Json;
using csvreportexercise.application.Models.V1;
using Microsoft.Extensions.Options;

namespace csvreportexercise.application.Services.V1.Interfaces;

public class OpenLibraryService(HttpClient httpClient, IOptions<OpenLibrarySettings> openLibrarySection) : IOpenLibraryService
{
    public async Task<OpenLibraryBookInfo?> GetBookInfo(string isbn)
    {
        string uri = openLibrarySection.Value.BooksInfoUri;
        
        using HttpResponseMessage response = await httpClient.GetAsync($"{uri}{isbn}");

        var jsonResponse = await response.Content.ReadAsStringAsync();

        OpenLibraryBookInfo? bookInfo = JsonSerializer.Deserialize<OpenLibraryBookInfo>(jsonResponse);

        return bookInfo;
    }
}