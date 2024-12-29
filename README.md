# HttpResponseBuilder

HttpResponseBuilder is a .NET package designed to streamline the process of returning structured and appropriate HTTP responses based on status codes in ASP.NET Core applications. The package provides a simple API to validate responses and generate corresponding `IActionResult` objects based on status codes.

## Features
- Out of box generic reponse model which can be used to bind incoming repsonse to this model.
- Validates successful response.`EnsureSuccessStatusCode`.
- Returns appropriate `IActionResult` types (`OkResult`, `NotFoundResult`, `BadRequestResult`, etc.).
- Supports both generic and non-generic response validation.
- Ensures proper handling of null responses and status code mismatches.

## Installation

You can install the `HttpResponseBuilder` package from NuGet:

```bash
dotnet add package HttpResponseBuilder
