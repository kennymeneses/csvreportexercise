using csvreportexercise.application.Services.V1.Interfaces;
using Microsoft.AspNetCore.Http;

namespace csvreportexercise.application.Services.V1;

public class FormFileService : IFormFileService
{
    public async Task<List<string>> GetIsbnListAsync(IFormFile file)
    {
        List<string> isbnList = new List<string>();

        if (file.Length > 0)
        {
            using var stream = new StreamReader(file.OpenReadStream());

            string line = string.Empty;

            while ((line = await stream.ReadLineAsync()) != null)
            {
                string[] isbns = line.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                
                foreach (var isbn in isbns)
                {
                    isbnList.Add(isbn.Trim());
                }
            }
        }
        return isbnList;
    }
}