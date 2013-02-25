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
                throw new ConfigurationErrorsException("apiKey must be configured");
            request.Headers.Add(HttpRequestHeader.Authorization, "key " + apiKey);
        }

        public static void HandleProxySettings(this WebRequest request)
        {
            if (request.Proxy != null && request.Proxy.Credentials == null)
            {
                request.Proxy.Credentials = CredentialCache.DefaultCredentials;
            }
        }
    }
}
