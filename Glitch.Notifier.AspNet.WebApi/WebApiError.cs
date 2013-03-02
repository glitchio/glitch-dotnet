using System.Linq;
using System.Web;
using System.Web.Http.Filters;
using System.Net.Http;
using Glitch.Notifier.AspNet.Utils;

namespace Glitch.Notifier.AspNet.WebApi
{
    public class WebApiError : ErrorContextWrapper
    {
        private readonly HttpActionExecutedContext _context;

        public WebApiError(HttpActionExecutedContext context)
            : base(new Error(context.Exception))
        {
            _context = context;
            Error.WithLocation(GetController() + "#" + GetAction());
        }

        public WebApiError WithContextData()
        {
            return WithHttpHeaders()
                .WithHttpMethod()
                .WithUrl()
                .WithRouteData()
                .WithQueryString()
                .WithCurrentUser()
                .WithHttpStatusCode()
                .WithCookies();
        }

        public WebApiError WithHttpHeaders()
        {
            Error.With("HttpHeaders", _context.Request.Headers.ToDictionary(h => h.Key, h => h.Value));
            return this;
        }

        public WebApiError WithHttpStatusCode()
        {
            Error.With("HttpStatusCode", _context.Response.StatusCode);
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

        public WebApiError WithCookies()
        {
            if (HttpContext.Current == null) return this;
            Error.With("Cookies", new HttpContextWrapper(HttpContext.Current).GetCookies());
            return this;
        }

        public WebApiError WithCurrentUser()
        {
            if(Glitch.Config.CurrentUserRetriever != null)
            {
                 Error.WithUser(Glitch.Config.CurrentUserRetriever());
                return this;
            }
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
            Error.With("ServerInfo", ServerUtils.GetServerInfo());
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

        public WebApiError WithRouteData()
        {
            Error.With("RouteData", _context.ActionContext.ControllerContext.RouteData.Values);
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
