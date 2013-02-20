using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;

namespace Glitch.Notifier.AspNet.WebForms
{
    public class WebFormsError: ErrorContextWrapper
    {
        private readonly HttpContext _context;

        public WebFormsError(Exception exception, HttpContext context) 
            : base(new Error(exception))
        {
            _context = context;
        }

        public WebFormsError WithContextData()
        {
            return
                 WithHttpHeaders()
                .WithQueryString()
                .WithUrl()
                .WithHttpMethod()
                .WithCurrentUser();
        }

        public WebFormsError WithHttpHeaders()
        {
            if (_context == null) return this;
            var sourceHeaders = _context.Request.Headers;
            var headers = ToDictionary(sourceHeaders);
            Error.With("HttpHeaders", headers);
            return this;
        }

        public WebFormsError WithUrl()
        {
            if (_context == null) return this;
            Error.With("Url", _context.Request.Url.ToString());
            return this;
        }

        public WebFormsError WithQueryString()
        {
            if (_context == null) return this;
            var source = _context.Request.QueryString;
            if (source.AllKeys.Any())
            {
                var queryString = ToDictionary(source);
                Error.With("QueryString", queryString);
            }
            return this;
        }

        public WebFormsError WithCurrentUser()
        {
            if (_context == null) return this;
            var user = "anonymous";
            if (_context.User != null)
            {
                user = _context.User.Identity.Name;
            }

            return WithUser(user);
        }

        public WebFormsError WithUser(string user)
        {
            Error.With("User", user);
            return this;
        }

        public WebFormsError WithHttpMethod()
        {
            Error.With("HttpMethod", _context.Request.HttpMethod);
            return this;
        }

        public WebFormsError WithErrorProfile(string errorProfile)
        {
            Error.WithErrorProfile(errorProfile);
            return this;
        }

        private static Dictionary<string, string> ToDictionary(NameValueCollection sourceHeaders)
        {
            return sourceHeaders.Cast<string>()
                .Select(s => new { Key = s, Value = sourceHeaders[s] })
                .ToDictionary(p => p.Key, p => p.Value);
        }
    }
}
