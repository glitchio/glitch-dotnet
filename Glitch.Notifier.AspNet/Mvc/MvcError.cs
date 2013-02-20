using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Glitch.Notifier.AspNet.Mvc
{
    public class MvcError : ErrorContextWrapper
    {
        private readonly ExceptionContext _context;

        public MvcError(ExceptionContext context)
            : base(new Error(context.Exception))
        {
            _context = context;
        }

        public MvcError WithContextData()
        {
            return
                    WithCurrentUser()
                    .WithHttpHeaders()
                    .WithHttpMethod()
                    .WithUrl()
                    .WithController()
                    .WithAction()
                    .WithQueryString();
        }

        public MvcError WithHttpHeaders()
        {
            var sourceHeaders = _context.HttpContext.Request.Headers;
            var headers = ToDictionary(sourceHeaders);
            Error.With("HttpHeaders", headers);
            return this;
        }

        public MvcError WithUrl()
        {
            Error.With("Url", _context.HttpContext.Request.Url.ToString());
            return this;
        }

        public MvcError WithQueryString()
        {
            var source = _context.HttpContext.Request.QueryString;
            if (source.AllKeys.Any())
            {
                var queryString = ToDictionary(source);
                Error.With("QueryString", queryString);
            }
            return this;
        }

        public MvcError WithCurrentUser()
        {
            var user = "anonymous";
            if (_context.HttpContext.User != null)
            {
                user = _context.HttpContext.User.Identity.Name;
            }

            return WithUser(user);
        }

        public MvcError WithUser(string user)
        {
            Error.With("User", user);
            return this;
        }

        public MvcError WithHttpMethod()
        {
            Error.With("HttpMethod", HttpContext.Current.Request.HttpMethod);
            return this;
        }

        public MvcError WithController()
        {
            Error.With("Controller", _context.RouteData.Values["controller"]);
            return this;
        }

        public MvcError WithAction()
        {
            Error.With("Action", _context.RouteData.Values["action"]);
            return this;
        }

        public MvcError WithErrorProfile(string errorProfile)
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
