using System;
using System.Web;

namespace Glitch.Notifier.AspNet.WebForms
{
    public static class GlitchErrorFactoryExtensions
    {
        public static WebFormsError WebFormsError(this GlitchErrorFactory factory, Exception exception, HttpContext context)
        {
            return new WebFormsError(exception, context);
        }
    }
}