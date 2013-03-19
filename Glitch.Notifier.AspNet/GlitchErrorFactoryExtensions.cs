using System;
using System.Web;

namespace Glitch.Notifier.AspNet
{
    public static class GlitchErrorFactoryExtensions
    {
        public static HttpContextError HttpContextError(this GlitchErrorFactory factory, Exception exception, HttpContext context)
        {
           return HttpContextError(factory, exception, new HttpContextWrapper(context));
        }

        public static HttpContextError HttpContextError(this GlitchErrorFactory factory, Exception exception, HttpContextBase context)
        {
            if (context == null) throw new ArgumentNullException("context");
            return new HttpContextError(exception, context);
        }

        public static HttpContextError HttpContextError(this GlitchErrorFactory factory, Exception exception, string errorProfile)
        {
            return HttpContextError(factory, exception, HttpContext.Current).WithErrorProfile(errorProfile);
        }

        public static HttpContextError HttpContextError(this GlitchErrorFactory factory, HttpException exception, HttpContext context)
        {
            return HttpContextError(factory, exception, new HttpContextWrapper(context));
        }

        public static HttpContextError HttpContextError(this GlitchErrorFactory factory, HttpException exception, HttpContextBase context)
        {
            if (context == null) throw new ArgumentNullException("context");
            return new HttpContextError(exception, context);
        }

        public static HttpContextError HttpContextError(this GlitchErrorFactory factory, HttpException exception, string errorProfile)
        {
            return HttpContextError(factory, exception, HttpContext.Current).WithErrorProfile(errorProfile);
        }
    }
}