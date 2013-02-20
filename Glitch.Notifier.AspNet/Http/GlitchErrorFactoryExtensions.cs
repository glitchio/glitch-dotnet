using System.Web.Http.Filters;

namespace Glitch.Notifier.AspNet.Http
{
    public static class GlitchErrorFactoryExtensions
    {
        public static WebApiError WebApiError(this GlitchErrorFactory factory, HttpActionExecutedContext context)
        {
            return new WebApiError(context);
        }
    }
}