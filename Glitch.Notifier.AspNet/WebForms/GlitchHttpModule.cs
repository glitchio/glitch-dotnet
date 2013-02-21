using System;
using System.Web;
using Glitch.Notifier.AspNet.Shared;

namespace Glitch.Notifier.AspNet.WebForms
{
    public class GlitchHttpModule:IHttpModule
    {
        public void Init(HttpApplication context)
        {
            context.Error += ContextError;
        }

        static void ContextError(object sender, EventArgs e)
        {
            var exception = HttpContext.Current.Server.GetLastError();

            Glitch.Factory.WebFormsError(exception, HttpContext.Current)
                  .WithContextData()
                  .Send();

            HttpContext.Current.Server.ClearError();
        }

        public void Dispose()
        {
          
        }
    }
}
