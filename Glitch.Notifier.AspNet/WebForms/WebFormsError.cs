using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using Glitch.Notifier.AspNet.Shared;

namespace Glitch.Notifier.AspNet.WebForms
{
    public class WebFormsError: HttpContextError
    {
        public WebFormsError(Exception exception, HttpContext context)
            : base(exception, new HttpContextWrapper(context), "v1.net.webforms")
        {
        
        }

        public WebFormsError WithContextData()
        {
            return
                HttpContextErrorExtensions.WithContextData(this);
        }
       
    }
}
