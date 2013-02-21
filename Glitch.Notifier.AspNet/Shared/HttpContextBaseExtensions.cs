﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Web;

namespace Glitch.Notifier.AspNet.Shared
{
    static class HttpContextBaseExtensions
    {
        public static string GetUrl(this HttpContextBase context)
        {
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
            if (context.User != null)
            {
                user = context.User.Identity.Name;
            }
            return user;
        }

        public static string GetHttpMethod(this HttpContextBase context)
        {
            return context.Request.HttpMethod;
        }

        private static Dictionary<string, string> ToDictionary(NameValueCollection sourceHeaders)
        {
            return sourceHeaders.Cast<string>()
                .Select(s => new { Key = s, Value = sourceHeaders[s] })
                .ToDictionary(p => p.Key, p => p.Value);
        }
    }
}