using System;
using System.Web;

namespace Glitch.Notifier.AspNet
{
    public class GlitchHttpModule:IHttpModule
    {
        public GlitchHttpModule()
        {
            ErrorProfile = "v1.net.asp";
        }

        public string ErrorProfile { get; set; }
        public void Init(HttpApplication context)
        {
            context.Error += ContextError;
        }

        void ContextError(object sender, EventArgs e)
        {
            var exception = HttpContext.Current.Server.GetLastError();

            Glitch.Factory.HttpContextError(exception, HttpContext.Current, ErrorProfile)
                  .WithContextData()
                  .Send();

            HttpContext.Current.Server.ClearError();
        }

        public void Dispose()
        {
          
        }
    }
}
