using csvreportexercise.application.Models.V1;

namespace csvreportexercise.application.Services.V1.Interfaces;

public interface IOpenLibraryService
{
    Task<OpenLibraryBookInfo?> GetBookInfo(string isbn);
}