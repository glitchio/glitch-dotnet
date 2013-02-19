using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Glitch.Notifier.AspNet
{
    public static class ErrorExtensions
    {
        public static Error WithHttpHeaders(this Error error)
        {
            return error;
        }

        public static Error WithUrl(this Error error)
        {
            error.With("Url", HttpContext.Current.Request.Url.ToString());
            return error;
        }

        public static Error WithQueryString(this Error error)
        {
            return error;
        }

        public static Error WithCurrentUser(this Error error)
        {
            string user = "anonymous";
            if(HttpContext.Current.User != null)
            {
                user = HttpContext.Current.User.Identity.Name;
            }

            return WithUser(error, user);
        }

        public static Error WithUser(this Error error, string user)
        {
            error.With("User", user);
            return error;
        }
    }
}
