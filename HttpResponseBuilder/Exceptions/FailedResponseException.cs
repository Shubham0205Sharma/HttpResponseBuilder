
namespace HttpResponseBuilder.Exceptions
{
    /// <summary>
    /// Notifies failure of Response.
    /// </summary>
    public class FailedResponseException : Exception
    {
        public FailedResponseException(string message) : base(message)
        {
            
        }
    }
}
