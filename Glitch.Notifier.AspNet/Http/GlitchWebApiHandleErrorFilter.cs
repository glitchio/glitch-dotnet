using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;

namespace Glitch.Notifier.AspNet.Http
{
    public class GlitchWebApiHandleErrorFilter : IExceptionFilter
    {
        public bool AllowMultiple
        {
            get { return true; }
        }
        public Task ExecuteExceptionFilterAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
        {
            return Glitch.Factory.WebApiError(actionExecutedContext)
                .WithContextData()
                .SendAsync();
        }
    }
}
