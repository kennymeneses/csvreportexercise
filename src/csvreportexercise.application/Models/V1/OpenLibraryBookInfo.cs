namespace csvreportexercise.application.Models.V1;

public class OpenLibraryBookInfo
{
    public string BibKey { get; set; } = string.Empty;
    public string InfoUrl { get; set; } = string.Empty;
    public string Preview { get; set; } = string.Empty;
    public string PreviewUrl { get; set; } = string.Empty;
    public string ThumbnailUrl { get; set; } = string.Empty;
    public Details Details { get; set; } = new();
}

public class Details
{
    public List<string> Publishers { get; set; } = [];
    public int NumberOfPages { get; set; }
    public string Subtitle { get; set; } = string.Empty;
    public List<string> Isbn10 { get; set; } = [];
    public List<int> Covers { get; set; } = [];
    public List<string> LcClassifications { get; set; } = [];
    public string Key { get; set; } = string.Empty;
    public List<Author> Authors { get; set; }  = [];
    public string Ocaid { get; set; } = string.Empty;
    public List<string> PublishPlaces { get; set; }  = [];
    public List<string> Contributions { get; set; } = [];
    public List<string> Subjects { get; set; } = [];
    public List<Language> Languages { get; set; } = [];
    public string Pagination { get; set; } = string.Empty;
    public List<string> SourceRecords { get; set; } = [];
    public string Title { get; set; } = string.Empty;
    public List<string> DeweyDecimalClass { get; set; }  = [];
    public Note Notes { get; set; } = new();
    public Identifiers Identifiers { get; set; } = new();
    public string EditionName { get; set; } = string.Empty;
    public List<string> Lccn { get; set; } = [];
    public List<string> LocalId { get; set; } = [];
    public string PublishDate { get; set; } = string.Empty;
    public string PublishCountry { get; set; } = string.Empty;
    public string ByStatement { get; set; } = string.Empty;
    public List<Work> Works { get; set; }  = [];
    public TypeInfo Type { get; set; } = new();
    public int LatestRevision { get; set; }
    public int Revision { get; set; }
    public DateTimeInfo Created { get; set; } = new();
    public DateTimeInfo LastModified { get; set; } = new();
}

public class Author
{
    public string Key { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
}

public class Language
{
    public string Key { get; set; } = string.Empty;
}

public class Note
{
    public string Type { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
}

public class Identifiers
{
    public List<string> Librarything { get; set; } = [];
    public List<string> Wikidata { get; set; } = [];
    public List<string> Goodreads { get; set; } = [];
}

public class Work
{
    public string Key { get; set; } = string.Empty;
}

public class TypeInfo
{
    public string Key { get; set; } = string.Empty;
}

public class DateTimeInfo
{
    public string Type { get; set; } = string.Empty;
    public DateTime Value { get; set; }
}