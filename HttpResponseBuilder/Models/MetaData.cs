namespace HttpResponseBuilder.Models
{
    public class MetaData
    {
        public string RequestId { get; set; }
        public DateTime Timestamp { get; set; }
        public PaginationInfo Pagination { get; set; }
    }
}
