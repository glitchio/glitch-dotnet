using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Glitch.Notifier.AspNet
{
    public class WebFormsHttpModule:IHttpModule
    {
        public void Init(HttpApplication context)
        {
            context.Error += ContextError;
        }

        static void ContextError(object sender, EventArgs e)
        {
            var exception = HttpContext.Current.Server.GetLastError();

            Glitch.Notify(exception)
                  .WithCurrentUser()
                  .WithHttpHeaders()
                  .WithQueryString()
                  .WithUrl()
                  .WithErrorProfile("v1.net.webforms")
                  .Send();

            HttpContext.Current.Server.ClearError();
        }

        public void Dispose()
        {
          
        }
    }
}
