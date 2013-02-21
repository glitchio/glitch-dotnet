using System;
using System.Web;

namespace Glitch.Notifier.AspNet.WebForms
{
    public class WebFormsError: HttpError
    {
        public WebFormsError(Exception exception, HttpContext context)
            : base(exception, new HttpContextWrapper(context))
        {
        
        }

        public WebFormsError WithContextData()
        {
            return
                HttpContextErrorExtensions.WithContextData(this);
        }
       
    }
}
