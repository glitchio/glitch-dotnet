using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Glitch.Notifier.AspNet.Shared
{
    public class HttpContextError:ErrorContextWrapper
    {
        public HttpContextBase HttpContext { get; private set; }

        public HttpContextError(Exception exception, HttpContextBase httpContext)
            : base(new Error(exception))
        {
            HttpContext = httpContext;
        }
    }

    public static class HttpContextErrorExtensions
    {

        public static T WithContextData<T>(this T wrapper) where T:HttpContextError
        {
            return
                    wrapper.WithCurrentUser()
                    .WithHttpHeaders()
                    .WithHttpMethod()
                    .WithUrl()
                    .WithQueryString();
        }

        public static T WithHttpHeaders<T>(this T wrapper) where T:HttpContextError
        {
            wrapper.Error.With("HttpHeaders", wrapper.HttpContext.GetHttpHeaders());
            return wrapper;
        }

        public static T WithUrl<T>(this T wrapper) where T : HttpContextError
        {
            wrapper.Error.With("Url", wrapper.HttpContext.GetUrl());
            return wrapper;
        }

        public static T WithQueryString<T>(this T wrapper) where T : HttpContextError
        {
            wrapper.Error.With("HttpHeaders", wrapper.HttpContext.GetHttpHeaders());
            return wrapper;
        }

        public static T WithCurrentUser<T>(this T wrapper) where T : HttpContextError
        {
            return wrapper.WithUser(wrapper.HttpContext.GetCurrentUser());
        }

        public static T WithUser<T>(this T wrapper, string user) where T : HttpContextError
        {
            wrapper.Error.With("User",user);
            return wrapper;
        }

        public static T WithHttpMethod<T>(this T wrapper) where T : HttpContextError
        {
            wrapper.Error.With("HttpMethod", wrapper.HttpContext.GetHttpMethod());
            return wrapper;
        }

        public static T WithErrorProfile<T>(this T wrapper, string errorProfile) where T : HttpContextError
        {
            wrapper.Error.WithErrorProfile(errorProfile);
            return wrapper;
        }
    }
}
