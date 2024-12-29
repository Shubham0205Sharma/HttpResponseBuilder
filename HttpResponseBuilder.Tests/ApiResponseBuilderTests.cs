using HttpResponseBuilder.Builder;
using Microsoft.AspNetCore.Mvc;

namespace HttpResponseBuilder.Tests
{
    public class ApiResponseBuilderTests
    {
        [Fact]
        public void ValidateAndReturn_ShouldReturnOkResult_WhenStatusCodeMatches()
        {
            // Arrange
            var response = new { StatusCode = 200, Message = "Success" };

            // Act
            var result = ApiResponseBuilder.ValidateAndReturn(response, 200);

            // Assert
            Assert.IsType<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.Equal(response, okResult.Value);
        }

        // Test for ValidateAndReturn with null response
        [Fact]
        public void ValidateAndReturn_ShouldThrowArgumentNullException_WhenResponseIsNull()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => ApiResponseBuilder.ValidateAndReturn<object>(null, 200));
        }

        // Test for ValidateAndReturn with specific status code
        [Fact]
        public void ValidateAndReturn_ShouldReturnNotFoundResult_WhenStatusCodeIs404()
        {
            // Arrange
            var response = new { StatusCode = 404, Message = "Not Found" };

            // Act
            var result = ApiResponseBuilder.ValidateAndReturn(response, 404);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        // Test for ValidateAndReturn with different status code (201)
        [Fact]
        public void ValidateAndReturn_ShouldReturnCreatedResult_WhenStatusCodeIs201()
        {
            // Arrange
            var response = new { StatusCode = 201, Message = "Resource Created" };

            // Act
            var result = ApiResponseBuilder.ValidateAndReturn(response, 201);

            // Assert
            Assert.IsType<CreatedResult>(result);
        }
    }
}