using System.Text.Json.Serialization;

namespace csvreportexercise.application.Models.V1;


public class OpenLibraryBookInfo
{
    [JsonPropertyName("bib_key")]
    public string BibKey { get; set; } = string.Empty;

    [JsonPropertyName("info_url")]
    public string InfoUrl { get; set; } = string.Empty;

    [JsonPropertyName("preview")]
    public string Preview { get; set; } = string.Empty;

    [JsonPropertyName("preview_url")]
    public string PreviewUrl { get; set; } = string.Empty;

    [JsonPropertyName("thumbnail_url")]
    public string ThumbnailUrl { get; set; } = string.Empty;

    [JsonPropertyName("details")]
    public Details Details { get; set; } = new();
}

public class Details
{
    [JsonPropertyName("publishers")]
    public List<string> Publishers { get; set; } = [];

    [JsonPropertyName("number_of_pages")]
    public int NumberOfPages { get; set; }

    [JsonPropertyName("subtitle")]
    public string Subtitle { get; set; } = string.Empty;

    [JsonPropertyName("isbn_10")]
    public List<string> Isbn10 { get; set; } = [];

    [JsonPropertyName("covers")]
    public List<int> Covers { get; set; } = [];

    [JsonPropertyName("lc_classifications")]
    public List<string> LcClassifications { get; set; } = [];

    [JsonPropertyName("key")]
    public string Key { get; set; } = string.Empty;

    [JsonPropertyName("authors")]
    public List<Author> Authors { get; set; } = [];

    [JsonPropertyName("ocaid")]
    public string Ocaid { get; set; } = string.Empty;

    [JsonPropertyName("publish_places")]
    public List<string> PublishPlaces { get; set; } = [];

    [JsonPropertyName("contributions")]
    public List<string> Contributions { get; set; } = [];

    [JsonPropertyName("subjects")]
    public List<string> Subjects { get; set; } = [];

    [JsonPropertyName("languages")]
    public List<Language> Languages { get; set; } = [];

    [JsonPropertyName("pagination")]
    public string Pagination { get; set; } = string.Empty;

    [JsonPropertyName("source_records")]
    public List<string> SourceRecords { get; set; } = [];

    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;

    [JsonPropertyName("dewey_decimal_class")]
    public List<string> DeweyDecimalClass { get; set; } = [];

    //[JsonPropertyName("notes")]
    //public Note? Notes { get; set; }

    [JsonPropertyName("identifiers")]
    public Identifiers Identifiers { get; set; } = new();

    [JsonPropertyName("edition_name")]
    public string EditionName { get; set; } = string.Empty;

    [JsonPropertyName("lccn")]
    public List<string> Lccn { get; set; } = [];

    [JsonPropertyName("local_id")]
    public List<string> LocalId { get; set; } = [];

    [JsonPropertyName("publish_date")]
    public string PublishDate { get; set; } = string.Empty;

    [JsonPropertyName("publish_country")]
    public string PublishCountry { get; set; } = string.Empty;

    [JsonPropertyName("by_statement")]
    public string ByStatement { get; set; } = string.Empty;

    [JsonPropertyName("works")]
    public List<Work> Works { get; set; } = [];

    [JsonPropertyName("type")]
    public TypeInfo Type { get; set; } = new();

    [JsonPropertyName("latest_revision")]
    public int LatestRevision { get; set; }

    [JsonPropertyName("revision")]
    public int Revision { get; set; }

    [JsonPropertyName("created")]
    public DateTimeInfo Created { get; set; } = new();

    [JsonPropertyName("last_modified")]
    public DateTimeInfo LastModified { get; set; } = new();
    
    [JsonPropertyName("isbn_13")]
    public List<string> Isbn13 { get; set; } = new();

    [JsonPropertyName("full_title")]
    public string FullTitle { get; set; } = string.Empty;

    [JsonPropertyName("physical_format")]
    public string PhysicalFormat { get; set; } = string.Empty;
}

public class Author
{
    [JsonPropertyName("key")]
    public string Key { get; set; } = string.Empty;

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;
}

public class Language
{
    [JsonPropertyName("key")]
    public string Key { get; set; } = string.Empty;
}

public class Note
{
    [JsonPropertyName("type")]
    public string Type { get; set; } = string.Empty;

    [JsonPropertyName("value")]
    public string Value { get; set; } = string.Empty;
}

public class Identifiers
{
    [JsonPropertyName("librarything")]
    public List<string> LibraryThing { get; set; } = [];

    [JsonPropertyName("wikidata")]
    public List<string> Wikidata { get; set; } = [];

    [JsonPropertyName("goodreads")]
    public List<string> GoodReads { get; set; } = [];
}

public class Work
{
    [JsonPropertyName("key")]
    public string Key { get; set; } = string.Empty;
}

public class TypeInfo
{
    [JsonPropertyName("key")]
    public string Key { get; set; } = string.Empty;
}

public class DateTimeInfo
{
    [JsonPropertyName("type")]
    public string Type { get; set; } = string.Empty;

    [JsonPropertyName("value")]
    public DateTime Value { get; set; }
}