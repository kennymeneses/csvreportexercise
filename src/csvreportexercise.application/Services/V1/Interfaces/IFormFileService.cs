using Microsoft.AspNetCore.Http;

namespace csvreportexercise.application.Services.V1.Interfaces;

public interface IFormFileService
{
    Task<List<string>> GetIsbnListAsync(IFormFile file);
}