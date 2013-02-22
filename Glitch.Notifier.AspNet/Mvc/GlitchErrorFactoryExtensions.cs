using System.Web.Mvc;

namespace Glitch.Notifier.AspNet.Mvc
{
    public static class GlitchErrorFactoryExtensions
    {
        public static MvcError MvcError(this GlitchErrorFactory factory, ExceptionContext context, string errorProfile)
        {
            return new MvcError(context).WithErrorProfile(errorProfile);
        }
    }
}