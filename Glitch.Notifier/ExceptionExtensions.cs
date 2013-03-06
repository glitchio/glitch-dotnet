using System;
using System.IO;
using System.Net;
using ServiceStack.Text;

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

        public static Exception HandleWebException(this WebException exception)
        {
            return exception.WrapException();
        }

        public static Exception WrapException(this WebException exception)
        {
            var response = exception.Response as HttpWebResponse;
            if (response == null) return exception;
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                return new UnauthorizedAccessException("Glitch.io: Unauthorized. Check your apiKey please.", exception);
            }
            if (response.StatusCode == HttpStatusCode.BadRequest || response.StatusCode == HttpStatusCode.Conflict)
            {
                return new ArgumentException(GetError(response), exception);
            }
            if (response.StatusCode == HttpStatusCode.InternalServerError)
            {
                return new InvalidOperationException("Glitch.io: Internal Server error", exception);
            }
            return exception;
        }

        private static string GetError(HttpWebResponse response)
        {
            using (var responseStream = response.GetResponseStream())
            using (var reader = new StreamReader(responseStream))
            {
                var result = JsonObject.Parse(reader.ReadToEnd());
                var error = result.Object("message");
                if (error != null)
                    return error.ToString();
            }
            return "Error occurred.";
        }
    }
}
