using System;
using System.Diagnostics;
using System.Web;
using System.Web.Mvc;

namespace Glitch.Notifier.AspNet.Mvc
{
    public class GlitchHandleErrorAttribute : HandleErrorAttribute
    {
        public string ErrorProfile { get; set; }

        public GlitchHandleErrorAttribute(string errorProfile = "glitch/v1.net.mvc")
        {
            ErrorProfile = errorProfile;
        }

        public override void OnException(ExceptionContext exceptionContext)
        {
            if(HttpContext.Current != null)
            {
                HttpContext.Current.Items["Glitch.ErrorHandled"] = true;
            }
            try
            {
                Glitch.Factory.MvcError(exceptionContext, ErrorProfile)
                           .WithContextData()
                           .Send();
            }
            catch(Exception ex)
            {
                Trace.Write(ex.ToString());
            }
           
            base.OnException(exceptionContext);
        }
    }
}
