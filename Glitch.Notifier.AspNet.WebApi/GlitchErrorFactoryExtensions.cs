using System.Web.Http.Filters;

namespace Glitch.Notifier.AspNet.WebApi
{
    public static class GlitchErrorFactoryExtensions
    {
        public static WebApiError WebApiError(this GlitchErrorFactory factory, HttpActionExecutedContext context, string errorProfile)
        {
            return new WebApiError(context).WithErrorProfile(errorProfile);
        }
    }
}