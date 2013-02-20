using System.Web.Http.Filters;
using System.Web.Mvc;

namespace Glitch.Notifier.AspNet.Mvc
{
    public static class GlitchErrorFactoryExtensions
    {
        public static MvcError MvcError(this GlitchErrorFactory factory, ExceptionContext context)
        {
            return new MvcError(context);
        }
    }
}