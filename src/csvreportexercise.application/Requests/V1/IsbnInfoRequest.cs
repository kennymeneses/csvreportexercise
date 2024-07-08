using Microsoft.AspNetCore.Http;

namespace csvreportexercise.application.Requests.V1;

public sealed class IsbnInfoRequest
{
    public IFormFile File { get; init; } = null!;
}