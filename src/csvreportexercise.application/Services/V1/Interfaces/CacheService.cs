using csvreportexercise.application.Models.V1;
using Microsoft.Extensions.Caching.Memory;

namespace csvreportexercise.application.Services.V1.Interfaces;

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
        string cacheKey = "bookDictionaryStored";
        var listBookInfoResult = new List<BookInfo>();
        
        foreach (string isbn in isbnsList)
        {
            if (Cache.TryGetValue(isbn, out BookInfo bookInfo))
            {
                listBookInfoResult.Add(bookInfo);
            }
        }
        
        // if (Cache.TryGetValue(cacheKey, out Dictionary<string, BookInfo> bookDictionary))
        // {
        //     foreach (string isbn in isbnsList)
        //     {
        //         if (bookDictionary.TryGetValue(isbn, out BookInfo bookInfo))
        //         {
        //             listBookInfoResult.Add(bookInfo);
        //         }
        //     }
        // }

        return listBookInfoResult;
    }

    public void StoreBookInfo(BookInfo book)
    {
        Cache.Set(book.Isbn, book);
    }
}