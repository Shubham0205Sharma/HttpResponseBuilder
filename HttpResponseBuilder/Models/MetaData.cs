namespace HttpResponseBuilder.Models
{
    /// <summary>
    /// Class to hold Response metadata.
    /// </summary>
    public class MetaData
    {
        public string RequestId { get; set; }
        public DateTime Timestamp { get; set; }
        public PaginationInfo Pagination { get; set; }
    }
}
