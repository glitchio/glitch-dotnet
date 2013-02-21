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
            Error.WithLocation(GetController() + "#" + GetAction());
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
            Error.With("Controller", GetController());
            return this;
        }

        public MvcError WithAction()
        {
            Error.With("Action", GetAction());
            return this;
        }

        private string GetController()
        {
            return _exceptionContext.RouteData.Values["controller"] as string;
        }

        private string GetAction()
        {
            return _exceptionContext.RouteData.Values["action"] as string;
        }
    }
}
