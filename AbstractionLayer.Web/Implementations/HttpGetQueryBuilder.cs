using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace DoenaSoft.AbstractionLayer.WebServices;

/// <summary>
/// Builds an HTTP GET query string with the help of <see cref="Uri.EscapeDataString(string)"/>
/// </summary>
public sealed class HttpGetQueryBuilder
{
    private readonly Dictionary<string, string> _queryParameters;

    /// <summary />
    public HttpGetQueryBuilder()
    {
        _queryParameters = [];
    }

    /// <summary>
    /// Parses an existing query string with the help of <see cref="HttpUtility.ParseQueryString(string)"/> into an instance of <see cref="HttpGetQueryBuilder"/>.
    /// </summary>
    public static HttpGetQueryBuilder ParseQueryString(string query)
    {
        if (string.IsNullOrEmpty(query))
        {
            return new HttpGetQueryBuilder();
        }

        var instance = new HttpGetQueryBuilder();

        var keyValues = HttpUtility.ParseQueryString(query);

        foreach (var key in keyValues.AllKeys)
        {
            if (key != null)
            {
                var value = keyValues[key];

                instance.Add(key, value ?? string.Empty);
            }
        }

        return instance;
    }

    /// <summary>
    /// Gives access to the query parameters.
    /// </summary>
    public string this[string key]
    {
        get
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            return _queryParameters[key];
        }
        set
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            _queryParameters[key] = value ?? string.Empty;
        }
    }

    /// <summary>
    /// Adds a query parameter.
    /// </summary>
    /// <param name="key">The parameter key (will be URL-encoded)</param>
    /// <param name="value">The parameter value (will be URL-encoded)</param>
    public void Add(string key, string value)
    {
        if (key == null)
        {
            throw new ArgumentNullException(nameof(key));
        }

        _queryParameters.Add(key, value ?? string.Empty);
    }

    /// <summary>
    /// Checks if a query parameter with the specified key exists.
    /// </summary>
    public bool ContainsKey(string key)
    {
        if (key == null)
        {
            throw new ArgumentNullException(nameof(key));
        }

        return _queryParameters.ContainsKey(key);
    }

    /// <summary>
    /// Tries to get the value associated with the specified key.
    /// </summary>
    public bool TryGetValue(string key, out string value)
    {
        if (key == null)
        {
            throw new ArgumentNullException(nameof(key));
        }

        return _queryParameters.TryGetValue(key, out value);
    }

    /// <summary>
    /// Removes a query parameter.
    /// </summary>
    public bool Remove(string key)
    {
        if (key == null)
        {
            throw new ArgumentNullException(nameof(key));
        }

        return _queryParameters.Remove(key);
    }

    /// <summary>
    /// Removes all query parameters.
    /// </summary>
    public void Clear()
    {
        _queryParameters.Clear();
    }

    /// <summary>
    /// Gets the number of query parameters.
    /// </summary>
    public int Count
        => _queryParameters.Count;

    /// <summary>
    /// Returns the query string with proper URL encoding.
    /// Empty values are included as "key=" and null values are treated as empty strings.
    /// Special characters (including umlauts and ampersands) are properly percent-encoded.
    /// </summary>
    public override string ToString()
    {
        if (_queryParameters.Count == 0)
        {
            return string.Empty;
        }

        var queryBuilder = new StringBuilder();

        // Use OrderBy to ensure consistent output order
        var sortedParameters = _queryParameters.OrderBy(kvp => kvp.Key);

        var isFirst = true;

        foreach (var keyValue in sortedParameters)
        {
            if (!isFirst)
            {
                queryBuilder.Append('&');
            }

            // Uri.EscapeDataString properly encodes all special characters including:
            // - Umlauts (ä, ö, ü, etc.) -> percent-encoded
            // - Ampersands (&) -> %26
            // - Spaces -> %20
            // - Plus signs (+) -> %2B
            // - Equals (=) -> %3D
            queryBuilder.Append(Uri.EscapeDataString(keyValue.Key));
            queryBuilder.Append('=');

            var value = keyValue.Value ?? string.Empty;
            queryBuilder.Append(Uri.EscapeDataString(value));

            isFirst = false;
        }

        return queryBuilder.ToString();
    }

    /// <summary>
    /// Returns the query string without sorting the parameters (preserves insertion order as much as Dictionary allows).
    /// </summary>
    public string ToStringUnsorted()
    {
        if (_queryParameters.Count == 0)
        {
            return string.Empty;
        }

        var queryBuilder = new StringBuilder();

        var isFirst = true;

        foreach (var keyValue in _queryParameters)
        {
            if (!isFirst)
            {
                queryBuilder.Append('&');
            }

            queryBuilder.Append(Uri.EscapeDataString(keyValue.Key));
            queryBuilder.Append('=');

            var value = keyValue.Value ?? string.Empty;

            queryBuilder.Append(Uri.EscapeDataString(value));

            isFirst = false;
        }

        return queryBuilder.ToString();
    }
}