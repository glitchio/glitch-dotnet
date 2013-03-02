using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;

namespace Glitch.Notifier.AspNet.Utils
{
    public static class HttpContextBaseExtensions
    {
        public static string GetUrl(this HttpContextBase context)
        {
            if (context.Request == null || context.Request.Url == null) return null;
            return context.Request.Url.ToString();
        }

        public static IDictionary GetHttpHeaders(this HttpContextBase context)
        {
            var sourceHeaders = context.Request.Headers;
            return ToDictionary(sourceHeaders);
        }

        public static IDictionary GetQueryString(this HttpContextBase context)
        {
            var source = context.Request.QueryString;
            if (source.AllKeys.Any())
            {
                return ToDictionary(source);
            }
            return null;
        }

        public static string GetCurrentUser(this HttpContextBase context)
        {
            var user = "anonymous";
            if (context.User != null && context.User.Identity.IsAuthenticated)
            {
                user = context.User.Identity.Name;
            }
            return user;
        }

        public static int GetStatusCode(this HttpContextBase context)
        {
            return context.Response.StatusCode;
        }

        public static string GetHttpMethod(this HttpContextBase context)
        {
            return context.Request.HttpMethod;
        }

        public static Dictionary<string, string> GetClientInfo(this HttpContextBase context)
        {
            return new Dictionary<string, string>
                       {
                           {"Host", context.Request.UserHostName},
                           {"Ip", context.Request.UserHostAddress },
                       };
        }

        public static Dictionary<string, string> GetFormVariables(this HttpContextBase context)
        {
            var source = context.Request.Form;
            if (source.AllKeys.Any())
            {
                return ToDictionary(source);
            }
            return null;
        }

        public static Dictionary<string, string> GetServerVariables(this HttpContextBase context)
        {
            var source = context.Request.ServerVariables;
            if (source.AllKeys.Any())
            {
                return ToDictionary(source);
            }
            return null;
        }

        public static Dictionary<string, string> GetCookies(this HttpContextBase context)
        {
            var source = context.Request.Cookies;
            if (source.AllKeys.Any())
            {
                return ToDictionary(source);
            }
            return null;
        }

        public static string GetUrlReferer(this HttpContextBase context)
        {
            if (context.Request == null || context.Request.UrlReferrer == null)
                return null;
            return context.Request.UrlReferrer.ToString();
        }

        private static Dictionary<string, string> ToDictionary(NameValueCollection source)
        {
            var result = source.Cast<string>()
                .Select(s => new { Key = s, Value = source[s] })
                .ToDictionary(p => p.Key, p => p.Value);
            Glitch.Config.IgnoreContent.Filter(result);
            return result;
        }

        private static Dictionary<string, string> ToDictionary(HttpCookieCollection cookies)
        {
            var result = cookies.Cast<string>()
               .Select(s => new { Key = s, cookies[s].Value })
               .ToDictionary(p => p.Key, p => p.Value);
            Glitch.Config.IgnoreContent.Filter(result);
            return result;
        }
    }
}
