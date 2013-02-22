using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;

namespace Glitch.Notifier.AspNet.Http
{
    public class GlitchWebApiHandleErrorFilter : IExceptionFilter
    {
        public string ErrorProfile { get; set; }

        public GlitchWebApiHandleErrorFilter(string errorProfile = "v1.net.webapi")
        {
            ErrorProfile = errorProfile;
        }

        public bool AllowMultiple
        {
            get { return true; }
        }
        public Task ExecuteExceptionFilterAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
        {
            return Glitch.Factory.WebApiError(actionExecutedContext, ErrorProfile)
                .WithContextData()
                .SendAsync();
        }
    }
}
