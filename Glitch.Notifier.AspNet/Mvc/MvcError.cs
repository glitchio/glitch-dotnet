using System.Web.Mvc;

namespace Glitch.Notifier.AspNet.Mvc
{
    public class MvcError : HttpError
    {
        private readonly ExceptionContext _exceptionContext;

        public MvcError(ExceptionContext exceptionContext)
            : base(exceptionContext.Exception, exceptionContext.HttpContext, "v1.net.mvc")
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
