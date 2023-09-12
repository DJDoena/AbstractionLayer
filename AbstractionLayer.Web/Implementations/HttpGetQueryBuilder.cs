using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace DoenaSoft.AbstractionLayer.WebServices
{
    /// <summary>
    /// Builds an HTTP GET query string with the help of <see cref="Uri.EscapeDataString(string)"/>
    /// </summary>
    public sealed class HttpGetQueryBuilder
    {
        private readonly Dictionary<string, string> _queryParameters;

        /// <summary />
        public HttpGetQueryBuilder()
        {
            _queryParameters = new Dictionary<string, string>();
        }

        /// <summary>
        /// Parses an existing query string with the help of <see cref="HttpUtility.ParseQueryString(string)"/> into an instance of <see cref="HttpGetQueryBuilder"/>.
        /// </summary>
        public static HttpGetQueryBuilder ParseQueryString(string query)
        {
            var instance = new HttpGetQueryBuilder();

            var keyValues = HttpUtility.ParseQueryString(query);

            foreach (var key in keyValues.AllKeys)
            {
                instance.Add(key, keyValues[key]);
            }

            return instance;
        }

        /// <summary>
        /// Gives access to the query parameters.
        /// </summary>
        public string this[string key]
        {
            get => _queryParameters[key];
            set => _queryParameters[key] = value;
        }

        /// <summary>
        /// Adds a query parameter.
        /// </summary>
        public void Add(string key, string value)
            => _queryParameters.Add(key, value);

        /// <summary>
        /// Returns the query string.
        /// </summary>
        public override string ToString()
        {
            var queryBuilder = new StringBuilder();

            var keyIndex = 0;

            foreach (var keyValue in _queryParameters)
            {
                queryBuilder.Append(Uri.EscapeDataString(keyValue.Key));
                queryBuilder.Append('=');
                queryBuilder.Append(Uri.EscapeDataString(keyValue.Value));

                keyIndex++;

                if (keyIndex < _queryParameters.Count)
                {
                    queryBuilder.Append('&');
                }
            }

            return queryBuilder.ToString();
        }
    }
}