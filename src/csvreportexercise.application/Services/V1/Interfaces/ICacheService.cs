using csvreportexercise.application.Models.V1;
using Microsoft.Extensions.Caching.Memory;

namespace csvreportexercise.application.Services.V1.Interfaces;

public interface ICacheService
{
    IMemoryCache Cache { get; }

    List<BookInfo> GetBookListStored(List<string> sbnsList);

    void StoreBookInfo(BookInfo book);
}