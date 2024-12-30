# HttpResponseBuilder

`HttpResponseBuilder` is a NuGet package designed to standardize API responses in ASP.NET Core applications. 
This package provides a common response schema, out-of-the-box methods to ensure success status codes, 
and a helper method to validate and return appropriate status code results from controllers.

## Features

- **Unified API Response Schema**: Use `ApiResponse<T>` to standardize your API responses.
- **Validation**: Easily validate response status codes and ensure success.
- **Status Code Results**: Automatically return appropriate `IActionResult` based on the status code.
- **Extensibility:** Integrate easily with custom response models and scenarios.

---

## Installation

Install the NuGet package:

```bash
Install-Package HttpResponseBuilder
```

---

## Usage

### 1. Configure the Response Model

Use the `ApiResponse<T>` model to structure your API responses:

```csharp
using HttpResponseBuilder.Models;

[ApiController]
[Route("api/[controller]")]
public class SampleController : ControllerBase
{
    [HttpGet("{id}")]
    public IActionResult GetItem(int id)
    {
        if (id <= 0)
        {
            var errorResponse = new ApiResponse<object>
            {
                IsSuccess = false,
                Status = "error",
                StatusCode = 400,
                Error = new ApiError { Message = "Invalid ID provided." }
            };

            return new BadRequestObjectResult(errorResponse);
        }

        var item = new { Id = id, Name = "Sample Item" };

        var successResponse = new ApiResponse<object>
        {
            IsSuccess = true,
            Status = "success",
            StatusCode = 200,
            Data = item
        };

        return new OkObjectResult(successResponse);
    }
}
```

---

### 2. Ensure Success Status Code

The `EnsureSuccessStatusCode` method validates success responses and throws an exception if the response is not successful:

#### Example:

```csharp
using HttpResponseBuilder.Models;

var response = new ApiResponse<string>
{
    IsSuccess = true,
    StatusCode = 200,
    Data = "Operation completed successfully"
};

response.EnsureSuccessStatusCode(); // No exception is thrown

var failedResponse = new ApiResponse<string>
{
    IsSuccess = false,
    StatusCode = 500,
    Error = new ApiError { Message = "Internal server error." }
};

try
{
    failedResponse.EnsureSuccessStatusCode(); // Throws FailedResponseException
}
catch (FailedResponseException ex)
{
    Console.WriteLine(ex.Message);
}
```

---

### 3. Validate and Return Results

Use the `ValidateAndReturn` helper method to validate the status code and return appropriate `IActionResult`:

#### Example:

```csharp
using HttpResponseBuilder.Builders;

[HttpPost]
public IActionResult CreateItem([FromBody] ItemDto item)
{
    if (item == null)
    {
        var errorResponse = new ApiResponse<object>
        {
            IsSuccess = false,
            StatusCode = 400,
            Error = new ApiError { Message = "Item cannot be null." }
        };

        return ApiResponseBuilder.ValidateAndReturn(errorResponse);
    }

    var successResponse = new ApiResponse<ItemDto>
    {
        IsSuccess = true,
        StatusCode = 201,
        Data = item
    };

    return ApiResponseBuilder.ValidateAndReturn(successResponse);
}
```

---

### 4. Add Metadata to Responses

You can include additional metadata using the `Metadata` property:

#### Example:

```csharp
var response = new ApiResponse<object>
{
    IsSuccess = true,
    StatusCode = 200,
    Metadata = new MetaData
    {
        Timestamp = DateTime.UtcNow,
        AdditionalInfo = "Processed successfully"
    }
};
```

---

## Exception Handling

The `HttpResponseBuilder` library includes exceptions for handling specific scenarios:

- **`FailedResponseException`**: Thrown when a response status code is invalid or indicates failure.

Example:

```csharp
try
{
    var apiResponse = new ApiResponse<object> { IsSuccess = false, StatusCode = 404 };
    apiResponse.EnsureSuccessStatusCode();
}
catch (FailedResponseException ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}
```

---

## Testing the Package

A sample unit test project is included to help test and understand the usage of this package. To test:

1. Create a new ASP.NET Core Web API project.
2. Add the `HttpResponseBuilder` NuGet package.
3. Use the code examples provided above in your controllers.

---

## Contributing

We welcome contributions! Please fork this repository, make your changes, and submit a pull request.

---

## License

This project is licensed under the MIT License. See the LICENSE file for details.


