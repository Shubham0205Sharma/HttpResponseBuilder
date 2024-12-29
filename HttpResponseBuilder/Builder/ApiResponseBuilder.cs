using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace HttpResponseBuilder.Builder
{
    public class ApiResponseBuilder
    {
        // Method to validate and return appropriate IActionResult
        public static IActionResult ValidateAndReturn<T>(
            T response,
            int? expectedStatusCode = null,
            string statusCodeProperty = "StatusCode")
        {
            if (response == null)
                throw new ArgumentNullException(nameof(response), "The response cannot be null.");

            // Check the statusCodeProperty using reflection if necessary
            var actualStatusCode = GetStatusCode(response, statusCodeProperty);

            // If an expected status code is provided, validate it
            if (expectedStatusCode.HasValue && actualStatusCode != expectedStatusCode.Value)
            {
                throw new InvalidOperationException(
                    $"Expected status code {expectedStatusCode} but received {actualStatusCode}.");
            }

            // Return the appropriate IActionResult based on the status code
            return GetActionResultForStatusCode(actualStatusCode, response);
        }

        // Overload method that directly accepts a status code
        public static IActionResult ValidateAndReturn<T>(
            T response,
            int statusCode)
        {
            if (response == null)
                throw new ArgumentNullException(nameof(response), "The response cannot be null.");

            return GetActionResultForStatusCode(statusCode, response);
        }

        // Helper method to extract status code from response model using reflection
        private static int GetStatusCode<T>(T response, string statusCodeProperty)
        {
            var property = typeof(T).GetProperty(statusCodeProperty, BindingFlags.Public | BindingFlags.Instance);
            if (property == null)
                throw new ArgumentException($"The property '{statusCodeProperty}' does not exist in the response model.", nameof(statusCodeProperty));

            return (int)property.GetValue(response);
        }

        // Helper method to return IActionResult based on the status code
        private static IActionResult GetActionResultForStatusCode<T>(int statusCode, T response)
        {
            // Specific status code cases first
            return statusCode switch
            {
                201 => new CreatedResult("/created-resource", response),  // Created (201)
                204 => new NoContentResult(),  // No Content (204)

                // Success status codes (200-299)
                >= 200 and < 300 => new OkObjectResult(response),  // Success (200-299)

                // Error status codes
                400 => new BadRequestObjectResult(response),  // Bad Request (400)
                401 => new UnauthorizedObjectResult(response),  // Unauthorized (401)
                403 => new ForbidResult(),  // Forbidden (403)
                404 => new NotFoundObjectResult(response),  // Not Found (404)
                500 => new ObjectResult(response) { StatusCode = 500 },  // Internal Server Error (500)

                // Default case for other status codes
                _ => new ObjectResult(response) { StatusCode = statusCode },  // Other status codes
            };
        }
    }
}
