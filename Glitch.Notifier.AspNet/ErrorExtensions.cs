using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Web;

namespace Glitch.Notifier.AspNet
{
    public static class ErrorExtensions
    {
        public static Error WithHttpHeaders(this Error error)
        {
            CheckHttpContextCurrent();
            var sourceHeaders = HttpContext.Current.Request.Headers;
            var headers = ToDictionary(sourceHeaders);
            error.With("HttpHeaders", headers);
            return error;
        }

        private static Dictionary<string, string> ToDictionary(NameValueCollection sourceHeaders)
        {
            return sourceHeaders.Cast<string>()
                .Select(s => new { Key = s, Value = sourceHeaders[s] })
                .ToDictionary(p => p.Key, p => p.Value);
        }

        public static Error WithUrl(this Error error)
        {
            CheckHttpContextCurrent();
            error.With("Url", HttpContext.Current.Request.Url.ToString());
            return error;
        }

        public static Error WithQueryString(this Error error)
        {
            CheckHttpContextCurrent();
            var source = HttpContext.Current.Request.QueryString;
            if (source.AllKeys.Any())
            {
                var queryString = ToDictionary(source);
                error.With("QueryString", queryString);
            }
            return error;
        }

        public static Error WithCurrentUser(this Error error)
        {
            CheckHttpContextCurrent();
            var user = "anonymous";
            if (HttpContext.Current.User != null)
            {
                user = HttpContext.Current.User.Identity.Name;
            }

            return WithUser(error, user);
        }

        public static Error WithUser(this Error error, string user)
        {
            error.With("User", user);
            return error;
        }

        private static void CheckHttpContextCurrent()
        {
            if (HttpContext.Current == null) throw new InvalidOperationException("HttpContext.Current cannot be null");
        }
    }
}
