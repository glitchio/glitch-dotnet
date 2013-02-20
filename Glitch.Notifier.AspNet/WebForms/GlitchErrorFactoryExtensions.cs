using System;
using System.Web;
using System.Web.Http.Filters;

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