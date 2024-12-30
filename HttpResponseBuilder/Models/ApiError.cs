namespace HttpResponseBuilder.Models
{
    /// <summary>
    /// Api Error Object to capture api errors.
    /// </summary>
    public class ApiError
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public object Details { get; set; }
    }
}
