using System;
using System.Diagnostics;
using System.Web;

namespace Glitch.Notifier.AspNet
{
    public class GlitchHttpModule : IHttpModule
    {
        public GlitchHttpModule()
        {
            ErrorProfile = "glitch/v1.net.asp";
        }

        public string ErrorProfile { get; set; }
        public void Init(HttpApplication context)
        {
            context.Error += ContextError;
        }

        void ContextError(object sender, EventArgs e)
        {
            if (HttpContext.Current.Items.Contains("Glitch.ErrorHandled")) return;

            var exception = HttpContext.Current.Server.GetLastError();
            if (exception.InnerException == null) return;

            try
            {
                Glitch.Factory.HttpContextError(exception.InnerException, HttpContext.Current, ErrorProfile)
                      .WithContextData()
                      .Send();
            }
            catch (Exception ex)
            {
                Trace.Write(ex.ToString());
            }
        }

        public void Dispose()
        {

        }
    }
}
