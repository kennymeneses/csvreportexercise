using System.Text;
using System.Text.Json;
using csvreportexercise.application.Commons.V1.Enums;
using csvreportexercise.application.Models.V1;
using csvreportexercise.application.Services.V1.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace csvreportexercise.application.Services.V1;

public class CacheService : ICacheService
{
    public IMemoryCache Cache { get; }
    
    public CacheService()
    {
        var options = new MemoryCacheOptions
        {
            SizeLimit = 1024
        };

        Cache = new MemoryCache(options);
    }
    
    public List<BookInfo> GetBookListStored(List<string> isbnsList)
    {
        var listBookInfoResult = new List<BookInfo>();
        
        foreach (string isbn in isbnsList)
        {
            if (Cache.TryGetValue(isbn, out string bookInfoSerialized))
            {
                BookInfo bookInfo = JsonSerializer.Deserialize<BookInfo>(bookInfoSerialized!)!;
                bookInfo.Type = DataRetrievalType.Cache;
                listBookInfoResult.Add(bookInfo!);
            }
        }

        return listBookInfoResult;
    }

    public void StoreBookInfo(BookInfo book)
    {
        string jsonBook = JsonSerializer.Serialize(book);
        
        long sizeInBytes = Encoding.UTF8.GetByteCount(jsonBook);
        
        var cacheEntryOptions = new MemoryCacheEntryOptions()
            .SetSize(sizeInBytes);
        
        Cache.Set(book.Isbn, jsonBook, cacheEntryOptions);
    }
}