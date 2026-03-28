# DoenaSoft.AbstractionLayer.Web

Web and HTTP interface contracts for abstraction layer. This project contains **interface definitions only** and targets .NET Standard 2.0, .NET Framework 4.7.2, and .NET 10 to be usable from multiple framework versions.

Package Id: `DoenaSoft.AbstractionLayer.Web`

Targets: netstandard2.0, net472, net10.0

## Overview

This package contains **only the interface contracts** for web and HTTP abstractions. For production use, you'll also need the default implementations package: **`DoenaSoft.AbstractionLayer.Web.Default`** which contains `System.Net`-based implementations of these interfaces.

License: MIT

## Interfaces

The project provides the following interfaces for web operations:

### Core Interface

- **`IWebServices`** - The main entry point for creating web clients and requests. Provides factory methods:
  - `CreateWebRequest(string targetUrl, IWebProxy proxy, CultureInfo ci)` - Creates a web request
  - `CreateWebClient(IWebProxy proxy)` - Creates a web client

### Web Operation Interfaces

- **`IWebClient`** - Abstraction for `System.Net.WebClient`. Provides methods for downloading data, strings, and files.
- **`IWebRequest`** - Abstraction for `System.Net.WebRequest`. Provides methods for creating and configuring HTTP requests.
- **`IWebResponse`** - Abstraction for `System.Net.WebResponse`. Provides methods for reading HTTP responses.

## Usage

### In Production Code

Program against these interfaces and inject implementations at runtime. For the default `System.Net`-based implementations, reference the **`DoenaSoft.AbstractionLayer.Web.Default`** package:

```csharp
using DoenaSoft.AbstractionLayer.WebServices;

public class ApiClient
{
    private readonly IWebServices _webServices;

    public ApiClient(IWebServices webServices)
    {
        _webServices = webServices;
    }

    public string GetData(string url)
    {
        using var client = _webServices.CreateWebClient();
        return client.DownloadString(url);
    }
}

// In your application startup (using DoenaSoft.AbstractionLayer.Web.Default):
var webServices = new WebServices();
var apiClient = new ApiClient(webServices);
```

### In Unit Tests

Reference **only** this contracts package and provide test doubles to avoid network I/O:

```csharp
class FakeWebClient : IWebClient
{
    public string DownloadString(string url) => "{\"result\":\"test data\"}";
    // ... implement other members as needed
}

class FakeWebServices : IWebServices
{
    public IWebRequest CreateWebRequest(string targetUrl, IWebProxy proxy = null, CultureInfo ci = null)
        => throw new NotImplementedException();

    public IWebClient CreateWebClient(IWebProxy proxy = null)
        => new FakeWebClient();
}

// In your test
var fake = new FakeWebServices();
var apiClient = new ApiClient(fake);
var data = apiClient.GetData("https://api.example.com/data");
Assert.AreEqual("{\"result\":\"test data\"}", data);
```

## Testing Benefits

- Replace real network operations with in-memory fakes during tests to avoid network I/O
- Set up deterministic responses for tests
- Verify that your code handles network errors (timeouts, 404s, 500s) without making actual network calls
- Test retry logic and error handling without relying on external services
- Keep test projects lightweight by referencing only the contracts package

## Package Architecture

- **`DoenaSoft.AbstractionLayer.Web`** (this package) - Interface contracts only
- **`DoenaSoft.AbstractionLayer.Web.Default`** - Default `System.Net`-based implementations (depends on this package)

For production use, reference **`DoenaSoft.AbstractionLayer.Web.Default`**. For unit tests, reference only this contracts package.
