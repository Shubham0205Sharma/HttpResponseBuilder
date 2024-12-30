using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace HttpResponseBuilder.Builder
{
    /// <summary>
    /// ApiResponseBuilder class is meant to validate your statuscode and return appropriate StatusCodeResult for action methods.
    /// </summary>
    public class ApiResponseBuilder
    {
        /// <summary>
        /// Method to validate and return appropriate IActionResult
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="response"></param>
        /// <param name="statusCodeProperty"></param>
        /// <returns>Returns IActionResult</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IActionResult ValidateAndReturn<T>(
            T response,
            string statusCodeProperty = "StatusCode")
        {
            if (response == null)
                throw new ArgumentNullException(nameof(response), "The response cannot be null.");

            // Check the statusCodeProperty using reflection if necessary
            var actualStatusCode = GetStatusCode(response, statusCodeProperty);

            // Return the appropriate IActionResult based on the status code
            return GetActionResultForStatusCode(actualStatusCode, response);
        }

        /// <summary>
        /// Overload method that directly accepts a status code
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="response"></param>
        /// <param name="statusCode"></param>
        /// <returns>Returns IActionResult</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IActionResult ValidateAndReturn<T>(
            T response,
            int statusCode)
        {
            if (response == null)
                throw new ArgumentNullException(nameof(response), "The response cannot be null.");

            return GetActionResultForStatusCode(statusCode, response);
        }

        /// <summary>
        /// Helper method to extract status code from response model using reflection
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="response"></param>
        /// <param name="statusCodeProperty"></param>
        /// <returns>StatusCode Value of type Integer</returns>
        /// <exception cref="ArgumentException"></exception>
        private static int GetStatusCode<T>(T response, string statusCodeProperty)
        {
            var property = typeof(T).GetProperty(statusCodeProperty, BindingFlags.Public | BindingFlags.Instance);
            if (property == null)
                throw new ArgumentException($"The property '{statusCodeProperty}' does not exist in the response model.", nameof(statusCodeProperty));

            return (int)property.GetValue(response);
        }

        /// <summary>
        /// Helper method to return IActionResult based on the status code
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="statusCode"></param>
        /// <param name="response"></param>
        /// <returns>Returns IActionResult</returns>
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
                // Default case for other status codes
                _ => new ObjectResult(response) { StatusCode = statusCode },  // Other status codes
            };
        }
    }
}
