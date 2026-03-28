# DoenaSoft.AbstractionLayer.Web

Web and HTTP abstractions for easier unit testing of code that performs network operations.

Package Id: `DoenaSoft.AbstractionLayer.Web`

Target: netstandard2.0

Usage:

Use provided interfaces for HTTP requests so tests can substitute in mock implementations.

License: MIT

Interfaces

- Common abstractions include `IHttpClient`, `IHttpRequest`, and `IHttpResponse` (names vary depending on the needs of the codebase). These provide a small surface for sending requests and receiving responses without taking a dependency on `HttpClient` directly.
- `IHttpClient` typically exposes async send/receive methods and convenience helpers for common patterns (GET/POST with JSON).

Testing strategies

- Provide a fake `IHttpClient` implementation that returns preset responses for specific request URIs, enabling deterministic tests for retry, timeout and error handling logic.
- Use the abstractions to simulate transient failures and verify retry policies or fallback behavior without performing network calls.
