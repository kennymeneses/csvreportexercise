using csvreportexercise.application.Services.V1.Interfaces;
using Microsoft.AspNetCore.Http;

namespace csvreportexercise.application.Services.V1;

public class FormFileService : IFormFileService
{
    public async Task<Dictionary<string, int>> GetIsbnListAsync(IFormFile file)
    {
        List<string> isbnList = new List<string>();

        Dictionary<string, int> isbnDictionary = new Dictionary<string, int>();
        int lineNumber = 1;

        if (file.Length > 0)
        {
            using var stream = new StreamReader(file.OpenReadStream());

            string line = string.Empty;

            while ((line = await stream.ReadLineAsync()) != null)
            {
                string[] isbns = line.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                
                foreach (var isbn in isbns)
                {
                    if (!isbnDictionary.ContainsKey(isbn))
                    {
                        isbnDictionary.Add(isbn, lineNumber);
                    }
                }
                
                lineNumber++;
            }
        }
        return isbnDictionary;
    }
}