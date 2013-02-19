using System;
using System.Configuration;
using System.Net;

namespace Glitch.Notifier
{
    internal static class HttpRequestExtensions
    {
        public static void SetApiKey(this WebRequest request)
        {
            var apiKey = Glitch.Config.ApiKey;
            if (String.IsNullOrWhiteSpace(apiKey))
                throw new ConfigurationErrorsException("api Key must be configured");
            request.Headers.Add(HttpRequestHeader.Authorization, "key " + apiKey);
        }
    }
}
