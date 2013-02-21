using System.Web.Mvc;
using Glitch.Notifier.AspNet.Shared;

namespace Glitch.Notifier.AspNet.Mvc
{
    public class GlitchHandleErrorAttribute : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext exceptionContext)
        {
            base.OnException(exceptionContext);
            if (exceptionContext.ExceptionHandled)
            {
                Glitch.Factory.MvcError(exceptionContext)
                    .WithContextData()
                    .Send();
            }
        }
    }
}
