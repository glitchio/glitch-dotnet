using System.Web.Mvc;
using Glitch.Notifier.AspNet.Shared;

namespace Glitch.Notifier.AspNet.Mvc
{
    public class GlitchHandleErrorAttribute : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext exceptionContext)
        {
            Glitch.Factory.MvcError(exceptionContext)
                  .WithContextData()
                  .WithErrorProfile("v1.net.mvc")
                  .Send();

            base.OnException(exceptionContext);
        }
    }
}
