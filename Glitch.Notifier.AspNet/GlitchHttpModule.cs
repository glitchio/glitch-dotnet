using System;
using System.Diagnostics;
using System.Web;
using Glitch.Notifier.AspNet.ErrorContentFilters;
using Glitch.Notifier.Notifications;

namespace Glitch.Notifier.AspNet
{
    public class GlitchHttpModule : IHttpModule
    {
        private static volatile bool _applicationStarted;
        private static RegisteredObjectWrapper _registeredObject;
        private static readonly object ApplicationStartLock = new object();

        public GlitchHttpModule()
        {
            ErrorProfile = "glitch/v1.net.asp";
        }

        public string ErrorProfile { get; set; }
        public void Init(HttpApplication context)
        {
            if (!_applicationStarted)
            {
                lock (ApplicationStartLock)
                {
                    if (!_applicationStarted)
                    {
                        //content filter defaults
                        Glitch.Config.IgnoreContent.FromCookiesWithNamesContaining(".APXAUTH")
                            .FromFormVariablesWithNamesContaining("_VIEWSTATE")
                            .FromServerVariablesWithNamesContaining("AUTH_PASSWORD");

                        //Stop the worker thread gracefully when the appdomain is unloaded
                        _registeredObject = new RegisteredObjectWrapper(timeSpan => Glitch.Notifications.Stop(timeSpan));
                        _registeredObject.Register();

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
