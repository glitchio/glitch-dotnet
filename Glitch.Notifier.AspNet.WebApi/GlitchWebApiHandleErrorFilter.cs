using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;

namespace Glitch.Notifier.AspNet.WebApi
{
    public class GlitchWebApiHandleErrorFilter : IExceptionFilter
    {
        public string ErrorProfile { get; set; }

        public GlitchWebApiHandleErrorFilter(string errorProfile = "glitch/v1.net.asp.webapi")
        {
            ErrorProfile = errorProfile;
        }

        public bool AllowMultiple
        {
            get { return true; }
        }

        public Task ExecuteExceptionFilterAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
        {
            var completionSource = new TaskCompletionSource<bool>();
            try
            {
                Glitch.Factory.WebApiError(actionExecutedContext, ErrorProfile)
                    .WithContextData()
                    .Send();
            }
            catch (Exception ex)
            {
                Trace.Write(ex.ToString());
            }

            completionSource.SetResult(true);
            return completionSource.Task;
        }
    }
}
