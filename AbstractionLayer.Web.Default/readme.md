# DoenaSoft.AbstractionLayer.Web.Default

Default `System.Net`-based implementations of the web and HTTP abstraction interfaces defined in `DoenaSoft.AbstractionLayer.Web`. This package provides production-ready network operations for .NET Standard 2.0, .NET Framework 4.7.2, and .NET 10.

Package Id: `DoenaSoft.AbstractionLayer.Web.Default`

Targets: netstandard2.0, net472, net10.0

## Overview

This package contains the **default implementations** that wrap `System.Net` classes to implement the interfaces from `DoenaSoft.AbstractionLayer.Web`. Use this package in production code to perform actual web and HTTP operations.

**Dependencies:**
- `DoenaSoft.AbstractionLayer.Web` - Contains the interface contracts

License: MIT

## Implementations

This package provides concrete `System.Net`-based implementations and utilities:

### Core Implementation

- **`WebServices`** - Default implementation of `IWebServices`. Factory for creating web clients and requests.

### Web Operation Implementations

- **`WebClient`** - Wraps `System.Net.WebClient` for downloading data, strings, and files
- **`WebRequest`** - Wraps `System.Net.WebRequest` for creating and configuring HTTP requests
- **`WebResponse`** - Wraps `System.Net.WebResponse` for reading HTTP responses

### Utility Classes

- **`HttpGetQueryBuilder`** - Builds HTTP GET query strings with proper URL encoding. Handles special characters including:
  - Umlauts (ä, ö, ü) - percent-encoded
  - Ampersands (&) - encoded as %26
  - Spaces - encoded as %20
  - Plus signs (+) - encoded as %2B
  - All other special characters properly escaped

## Usage

### Basic Usage

Install both packages:
```
Install-Package DoenaSoft.AbstractionLayer.Web
Install-Package DoenaSoft.AbstractionLayer.Web.Default
```

Use the default implementations in production code:

```csharp
using DoenaSoft.AbstractionLayer.WebServices;

// Create the default WebServices instance
var webServices = new WebServices();

// Download a string
using var client = webServices.CreateWebClient();
var html = client.DownloadString("https://example.com");

// Create a web request
var request = webServices.CreateWebRequest("https://api.example.com/data");
using var response = request.GetResponse();
var data = response.ReadToEnd();
```

### Using HttpGetQueryBuilder

Build URL-encoded query strings with proper handling of special characters:

```csharp
using DoenaSoft.AbstractionLayer.WebServices;

var queryBuilder = new HttpGetQueryBuilder();
queryBuilder.Add("name", "Müller & Sons");
queryBuilder.Add("city", "München");
queryBuilder.Add("search", "value with spaces");

var queryString = queryBuilder.ToString();
// Result: name=M%C3%BCller%20%26%20Sons&city=M%C3%BCnchen&search=value%20with%20spaces

var url = $"https://api.example.com/search?{queryString}";

// Parse an existing query string
var parsed = HttpGetQueryBuilder.ParseQueryString("name=test&value=123");
var name = parsed["name"]; // "test"
```

### Dependency Injection

Inject `IWebServices` into your classes for testability:

```csharp
public class ApiClient
{
    private readonly IWebServices _webServices;

    public ApiClient(IWebServices webServices)
    {
        _webServices = webServices;
    }

    public string FetchData(string endpoint, Dictionary<string, string> parameters)
    {
        var queryBuilder = new HttpGetQueryBuilder();
        foreach (var param in parameters)
        {
            queryBuilder.Add(param.Key, param.Value);
        }

        var url = $"{endpoint}?{queryBuilder}";
        using var client = _webServices.CreateWebClient();
        return client.DownloadString(url);
    }
}

// In production (e.g., Program.cs or Startup.cs)
services.AddSingleton<IWebServices, WebServices>();

// In unit tests
var fakeWebServices = new FakeWebServices();
var apiClient = new ApiClient(fakeWebServices);
```

## Testing Benefits

- **Production:** Use this package for real network operations
- **Unit Tests:** Reference only `DoenaSoft.AbstractionLayer.Web` (contracts) and create test doubles
- Easily switch between real and fake implementations through dependency injection
- All implementations use the same interfaces, ensuring consistent behavior
- Test network error handling without making actual HTTP calls

## Package Architecture

- **`DoenaSoft.AbstractionLayer.Web`** - Interface contracts (required)
- **`DoenaSoft.AbstractionLayer.Web.Default`** (this package) - `System.Net`-based implementations

## Notes

- `HttpGetQueryBuilder` properly encodes all special characters using `Uri.EscapeDataString`
- Supports proxy configuration for all web operations
- Culture-aware request creation through `CultureInfo` parameter
- Thread-safe implementations for concurrent web operations
