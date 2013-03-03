using System;
using System.Diagnostics;
using System.Web;
using Glitch.Notifier.AspNet.ErrorContentFilters;

namespace Glitch.Notifier.AspNet
{
    public class GlitchHttpModule : IHttpModule
    {
        private static volatile bool _applicationStarted;
        private static readonly object ApplicationStartLock = new object();

        public GlitchHttpModule()
        {
            ErrorProfile = "glitch/v1.net.asp";
        }

        public string ErrorProfile { get; set; }
        public void Init(HttpApplication context)
        {
            if(!_applicationStarted)
            {
                lock (ApplicationStartLock)
                {
                    if(!_applicationStarted)
                    {
                        Glitch.Config.IgnoreContent.FromCookiesWithNamesContaining(".APXAUTH")
                            .FromFormVariablesWithNamesContaining("_VIEWSTATE")
                            .FromServerVariablesWithNamesContaining("AUTH_PASSWORD");

                        _applicationStarted = true;
                    }
                }
            }
            context.Error += ContextError;
        }

        void ContextError(object sender, EventArgs e)
        {
            if (HttpContext.Current.Items.Contains("Glitch.ErrorHandled")) return;

            var exception = HttpContext.Current.Server.GetLastError();
            if (exception.InnerException == null) return;

            try
            {
                Glitch.Factory.HttpContextError(exception.InnerException, ErrorProfile)
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
