using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Glitch.Notifier.AspNet.Shared;

namespace Glitch.Notifier.AspNet.Mvc
{
    public class MvcError : HttpContextError
    {
        private readonly ExceptionContext _exceptionContext;

        public MvcError(ExceptionContext exceptionContext)
            : base(exceptionContext.Exception, exceptionContext.HttpContext)
        {
            _exceptionContext = exceptionContext;
        }

        public MvcError WithContextData()
        {
            return
                HttpContextErrorExtensions.WithContextData(this)
                    .WithController()
                    .WithAction();
        }


        public MvcError WithController()
        {
            Error.With("Controller", _exceptionContext.RouteData.Values["controller"]);
            return this;
        }

        public MvcError WithAction()
        {
            Error.With("Action", _exceptionContext.RouteData.Values["action"]);
            return this;
        }
    }
}
