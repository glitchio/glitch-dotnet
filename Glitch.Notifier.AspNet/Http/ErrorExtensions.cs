using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Filters;

namespace Glitch.Notifier.AspNet.Http
{
    public static class GlitchErrorFactoryExtensions
    {
        public static WebApiError Error(this GlitchErrorFactory factory, HttpActionExecutedContext context)
        {
            return new WebApiError(context);
        }
    }

    public class WebApiError
    {
        private readonly HttpActionExecutedContext _context;
        private readonly Error _error;

        public WebApiError(HttpActionExecutedContext context)
        {
            _context = context;
            _error = new Error(context.Exception);
        }

        public void Send()
        {
            _error.Send();
        }

        public Task SendAsync()
        {
            return _error.SendAsync();
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
            _error.With("HttpHeaders", _context.Request.Headers.ToDictionary(h => h.Key, h => h.Value));
            return this;
        }

        public WebApiError WithController()
        {
            _error.With("Controller", _context.ActionContext.ControllerContext.RouteData.Values["controller"]);
            return this;
        }

        public WebApiError WithAction()
        {
            _error.With("Action", _context.ActionContext.ControllerContext.RouteData.Values["action"]);
            return this;
        }

        public WebApiError WithUrl()
        {
            _error.With("Url", _context.ActionContext.Request.RequestUri.ToString());
            return this;
        }

        public WebApiError WithHttpMethod()
        {
            _error.With("HttpMethod", _context.ActionContext.Request.Method.Method);
            return this;
        }


        public WebApiError WithErrorProfile(string errorProfile)
        {
            _error.WithErrorProfile(errorProfile);
            return this;
        }
    }
}
