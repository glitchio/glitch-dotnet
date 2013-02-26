using System;
using System.Configuration;
using System.Net;

namespace Glitch.Notifier
{
    internal static class HttpRequestExtensions
    {
        public static void SetApiKey(this WebRequest request, string apiKey)
        {
            if (String.IsNullOrWhiteSpace(apiKey))
                throw new ConfigurationErrorsException("apiKey must be configured");
            request.Headers.Add(HttpRequestHeader.Authorization, "key " + apiKey);
        }
    }
}
