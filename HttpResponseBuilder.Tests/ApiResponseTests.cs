using HttpResponseBuilder.Exceptions;
using HttpResponseBuilder.Models;

namespace HttpResponseBuilder.Tests
{
    public class ApiResponseTests
    {
        [Fact]
        public void EnsureSuccessStatusCode_ShouldThrowException_WhenIsSuccessIsFalse()
        {
            // Arrange
            var apiResponse = new ApiResponse<string>
            {
                IsSuccess = false,
                StatusCode = 200,
                Error = new ApiError { Message = "Some error occurred." }
            };

            // Act & Assert
            var exception = Assert.Throws<FailedResponseException>(() => apiResponse.EnsureSuccessStatusCode());
            Assert.Equal("The request was not successful. Status code: 200, Error: Some error occurred.", exception.Message);
        }
        [Fact]
        public void EnsureSuccessStatusCode_ShouldNotThrowException_WhenIsSuccessIsFalse()
        {
            // Arrange
            var apiResponse = new ApiResponse<string>
            {
                IsSuccess = true,
                StatusCode = 200,
            };

            // Act & Assert
            apiResponse.EnsureSuccessStatusCode(); //It should not throw any exception.
        }
    }
}
