using System;
using System.Web;
using Glitch.Notifier.AspNet.Utils;

namespace Glitch.Notifier.AspNet
{
    public class HttpContextError:ErrorContextWrapper
    {
        public HttpContextBase HttpContext { get; private set; }

        public HttpContextError(Exception exception, HttpContextBase httpContext)
            : base(new Error(exception))
        {
            HttpContext = httpContext;
            Error.WithLocation(HttpContext.GetUrl());
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
                    .WithQueryString()
                    .WithServerInfo()
                    .WithClientInfo()
                    .WithHttpStatusCode()
                    .WithCookies()
                    .WithForm()
                    .WithServerVariables()
                    .WithUrlReferer();
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

        public static T WithHttpStatusCode<T>(this T wrapper) where T : HttpContextError
        {
            wrapper.Error.With("HttpStatusCode", wrapper.HttpContext.GetStatusCode());
            return wrapper;
        }

        public static T WithQueryString<T>(this T wrapper) where T : HttpContextError
        {
            wrapper.Error.With("QueryString", wrapper.HttpContext.GetHttpHeaders());
            return wrapper;
        }

        public static T WithCurrentUser<T>(this T wrapper) where T : HttpContextError
        {
            var user = Glitch.Config.CurrentUserRetriever != null 
                              ? Glitch.Config.CurrentUserRetriever() 
                              : wrapper.HttpContext.GetCurrentUser();
            wrapper.Error.WithUser(user);
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

        public static T WithServerInfo<T>(this T wrapper) where T: HttpContextError
        {
            wrapper.Error.With("ServerInfo", ServerUtils.GetServerInfo());
            return wrapper;
        }

        public static T WithClientInfo<T>(this T wrapper) where T:HttpContextError
        {
            wrapper.Error.With("ClientInfo", wrapper.HttpContext.GetClientInfo());
            return wrapper;
        }

        public static T WithForm<T>(this T wrapper) where T:HttpContextError
        {
            wrapper.Error.With("Form", wrapper.HttpContext.GetFormVariables());
            return wrapper;
        }

        public static T WithCookies<T>(this T wrapper) where T:HttpContextError
        {
            wrapper.Error.With("Cookies", wrapper.HttpContext.GetCookies());
            return wrapper;
        }

        public static T WithServerVariables<T>(this T wrapper) where T:HttpContextError
        {
            wrapper.Error.With("ServerVariables", wrapper.HttpContext.GetServerVariables());
            return wrapper;
        }

        public static T WithUrlReferer<T>(this T wrapper) where T:HttpContextError
        {
            wrapper.Error.With("UrlReferer", wrapper.HttpContext.GetUrlReferer());
            return wrapper;
        }

    }
}
