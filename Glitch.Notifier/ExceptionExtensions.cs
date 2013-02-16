using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Newtonsoft.Json.Linq;

namespace Glitch.Notifier
{
    static class ExceptionExtensions
    {
        public static string GetStackTraceFirstLine(this Exception exception)
        {
            var stackTrace = exception.StackTrace;
            using(var stringReader = new StringReader(stackTrace))
            {
                return stringReader.ReadLine();
            }
        }

        public static void HandleWebException(this WebException exception)
        {
            var response = exception.Response as HttpWebResponse;
            if (response == null) return;
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                throw new UnauthorizedAccessException("Unauthorized", exception);
            }
            if (response.StatusCode == HttpStatusCode.BadRequest || response.StatusCode == HttpStatusCode.Conflict)
            {
                throw new ArgumentException(GetError(response), exception);
            }
            if (response.StatusCode == HttpStatusCode.InternalServerError)
            {
                throw new InvalidOperationException("Internal Server error", exception);
            }
        }

        private static string GetError(HttpWebResponse response)
        {
            using (var responseStream = response.GetResponseStream())
            using (var reader = new StreamReader(responseStream))
            {
                var result = JObject.Parse(reader.ReadToEnd());
                var error = result.Property("message");
                if (error != null)
                    return error.Value.ToString();
            }
            return "Error occurred.";
        }
    }
}
