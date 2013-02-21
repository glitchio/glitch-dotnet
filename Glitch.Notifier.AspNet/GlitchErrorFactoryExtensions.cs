using System;
using System.Web;

namespace Glitch.Notifier.AspNet
{
    public static class GlitchErrorFactoryExtensions
    {
        public static HttpError HttpContextError(this GlitchErrorFactory factory, Exception exception, HttpContext context)
        {
            if (context == null) throw new ArgumentNullException("context");
            return new HttpError(exception, new HttpContextWrapper(context));
        }

        public static HttpError HttpContextError(this GlitchErrorFactory factory, Exception exception)
        {
            return HttpContextError(factory, exception, HttpContext.Current);
        }
    }
}