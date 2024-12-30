using HttpResponseBuilder.Exceptions;

namespace HttpResponseBuilder.Models
{
    /// <summary>
    /// Generic Api Response model to hold response data.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ApiResponse<T>
    {
        public bool IsSuccess { get; set; } // Indicates whether the operation was successful
        public string Status { get; set; } // Optional, e.g., "success" or "error" etc.
        public int StatusCode { get; set; } // HTTP status code for the response.
        public T Data { get; set; } // Main payload for successful responses.
        public ApiError Error { get; set; } // Error details for failed responses.
        public MetaData Metadata { get; set; } // Additional metadata.

        public ApiResponse()
        {
            Metadata = new MetaData
            {
                Timestamp = DateTime.UtcNow
            };
        }

        /// <summary>
        /// Validate success status code and throw exception for failed statuscode.
        /// </summary>
        /// <exception cref="FailedResponseException"></exception>
        public void EnsureSuccessStatusCode()
        {
            if (!IsSuccess || StatusCode < 200 || StatusCode >= 400)
            {
                throw new FailedResponseException(
                    $"The request was not successful. Status code: {StatusCode}, Error: {Error?.Message ?? "No details provided."}");
            }
        }
    }
}
