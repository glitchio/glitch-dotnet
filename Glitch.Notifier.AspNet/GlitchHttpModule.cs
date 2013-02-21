using System;
using System.Web;

namespace Glitch.Notifier.AspNet
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

            Glitch.Factory.HttpContextError(exception, HttpContext.Current)
                  .WithContextData()
                  .Send();

            HttpContext.Current.Server.ClearError();
        }

        public void Dispose()
        {
          
        }
    }
}
