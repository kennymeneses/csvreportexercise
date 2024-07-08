namespace csvreportexercise.application.Commons.V1.Enums;

public enum DataRetrievalType
{
    /// <summary>
    /// The application has not yet encountered this ISBN number and the data is retrieved using the API.
    /// </summary>
    Server,
    
    /// <summary>
    /// The application has already encountered this ISBN number and the data is retrieved from a local cache.
    /// </summary>
    Cache
}