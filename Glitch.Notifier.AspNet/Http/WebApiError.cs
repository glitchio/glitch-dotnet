﻿using System.Linq;
using System.Web;
using System.Web.Http.Filters;
using System.Net.Http;

namespace Glitch.Notifier.AspNet.Http
{
    public class WebApiError : ErrorWrapper
    {
        private readonly HttpActionExecutedContext _context;

        public WebApiError(HttpActionExecutedContext context)
            : base(new Error(context.Exception), "v1.net.webapi")
        {
            _context = context;
            Error.WithLocation(GetController() + "#" + GetAction());
        }

        public WebApiError WithContextData()
        {
            return WithHttpHeaders()
                .WithHttpMethod()
                .WithUrl()
                .WithController()
                .WithAction()
                .WithQueryString()
                .WithCurrentUser();
        }

        public WebApiError WithHttpHeaders()
        {
            Error.With("HttpHeaders", _context.Request.Headers.ToDictionary(h => h.Key, h => h.Value));
            return this;
        }

        public WebApiError WithController()
        {
            Error.With("Controller", GetController());
            return this;
        }

        public WebApiError WithAction()
        {
            Error.With("Action", GetAction());
            return this;
        }

        public WebApiError WithUrl()
        {
            Error.With("Url", _context.ActionContext.Request.RequestUri.ToString());
            return this;
        }

        public WebApiError WithHttpMethod()
        {
            Error.With("HttpMethod", _context.ActionContext.Request.Method.Method);
            return this;
        }

        public WebApiError WithCurrentUser()
        {
            if (HttpContext.Current == null) return this;
            Error.WithUser(new HttpContextWrapper(HttpContext.Current).GetCurrentUser());
            return this;
        }

        public WebApiError WithQueryString()
        {
            var queryString = _context.Request.GetQueryNameValuePairs().ToDictionary(q => q.Key, q => q.Value);
            if (queryString.Keys.Count > 0)
            {
                Error.With("QueryString", queryString);
            }
            return this;
        }

        public WebApiError WithServerInfo()
        {
            Error.With("ServerInfo", Utils.GetServerInfo());
            return this;
        }

        public WebApiError WithClientInfo()
        {
            if (HttpContext.Current == null) return this;
            Error.With("ClientInfo", new HttpContextWrapper(HttpContext.Current).GetClientInfo());
            return this;
        }

        public WebApiError WithErrorProfile(string errorProfile)
        {
            Error.WithErrorProfile(errorProfile);
            return this;
        }

        private string GetController()
        {
            return _context.ActionContext.ControllerContext.RouteData.Values["controller"] as string;
        }

        private string GetAction()
        {
            return _context.ActionContext.ControllerContext.RouteData.Values["action"] as string;
        }
    }
}
