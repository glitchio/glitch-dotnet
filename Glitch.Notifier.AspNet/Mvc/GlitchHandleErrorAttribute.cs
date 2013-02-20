using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Glitch.Notifier.AspNet.Mvc
{
    public class GlitchHandleErrorAttribute : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext exceptionContext)
        {
            Glitch.Notify(exceptionContext.Exception)
                  .WithHttpContextData()
                  .With("Controller", exceptionContext.RouteData.Values["controller"])
                  .With("Action", exceptionContext.RouteData.Values["action"])
                  .WithErrorProfile("v1.net.mvc")
                  .Send();

            base.OnException(exceptionContext);
        }
    }
}
