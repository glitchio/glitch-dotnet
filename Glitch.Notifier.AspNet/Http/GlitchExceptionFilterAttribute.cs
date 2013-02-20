using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Http.Filters;

namespace Glitch.Notifier.AspNet.Http
{
    public class GlitchWebApiHandleErrorAttribute:ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            Glitch.Factory.Error(actionExecutedContext)
                 .WithContextData()
                 .WithErrorProfile("v1.net.webapi")
                 .Send();

            base.OnException(actionExecutedContext);
        }
    }
}
