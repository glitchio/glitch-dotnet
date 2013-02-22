using System.Web.Mvc;

namespace Glitch.Notifier.AspNet.Mvc
{
    public class GlitchHandleErrorAttribute : HandleErrorAttribute
    {
        public string ErrorProfile { get; set; }
        
        public GlitchHandleErrorAttribute(string errorProfile = "v1.net.mvc")
        {
            ErrorProfile = errorProfile;
        }

        public override void OnException(ExceptionContext exceptionContext)
        {
            base.OnException(exceptionContext);
            if (exceptionContext.ExceptionHandled)
            {
                Glitch.Factory.MvcError(exceptionContext, ErrorProfile)
                    .WithContextData()
                    .Send();
            }
        }
    }
}
