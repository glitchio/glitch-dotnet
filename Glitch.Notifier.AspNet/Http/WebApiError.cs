using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Filters;

namespace Glitch.Notifier.AspNet.Http
{
    public class WebApiError : ErrorContextWrapper
    {
        private readonly HttpActionExecutedContext _context;

        public WebApiError(HttpActionExecutedContext context)
            :base(new Error(context.Exception), "v1.net.webapi")
        {
            _context = context;
        }

        public WebApiError WithContextData()
        {
            WithHttpHeaders();
            WithHttpMethod();
            WithUrl();
            WithController();
            WithAction();
            return this;
        }

        public WebApiError WithHttpHeaders()
        {
            Error.With("HttpHeaders", _context.Request.Headers.ToDictionary(h => h.Key, h => h.Value));
            return this;
        }

        public WebApiError WithController()
        {
            Error.With("Controller", _context.ActionContext.ControllerContext.RouteData.Values["controller"]);
            return this;
        }

        public WebApiError WithAction()
        {
            Error.With("Action", _context.ActionContext.ControllerContext.RouteData.Values["action"]);
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


        public WebApiError WithErrorProfile(string errorProfile)
        {
            Error.WithErrorProfile(errorProfile);
            return this;
        }
    }
}
