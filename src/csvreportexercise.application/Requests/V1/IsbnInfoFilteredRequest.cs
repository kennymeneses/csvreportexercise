using csvreportexercise.application.Models.V1;

namespace csvreportexercise.application.Requests.V1;

public sealed class IsbnInfoFilteredRequest
{
    public ICollection<BookInfo> BooksStored = new List<BookInfo>();
    public Dictionary<string, int> IsbnDict = new ();
}