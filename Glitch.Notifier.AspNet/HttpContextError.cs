using System;
using System.Web;
using Glitch.Notifier.AspNet.Utils;

namespace Glitch.Notifier.AspNet
{
    public class HttpContextError:ErrorContextWrapper
    {
        public HttpContextBase HttpContext { get; private set; }
        public HttpException HttpException { get; private set; }

        public HttpContextError(HttpException exception, HttpContextBase httpContext)
            : base(new Error(exception.GetBaseException()))
        {
            HttpContext = httpContext;
            HttpException = exception;
            Error.WithLocation(HttpContext.GetUrl())
                .SetPlatform("ASP.NET");
        }

        public HttpContextError(Exception exception, HttpContextBase httpContext)
            : this(new HttpException(null, exception), httpContext )
        {
           
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
            var headers = wrapper.HttpContext.GetHttpHeaders();
            Glitch.Config.IgnoreContent.Filter("HttpHeaders", headers);
            wrapper.Error.With("HttpHeaders", headers);
            return wrapper;
        }

        public static T WithUrl<T>(this T wrapper) where T : HttpContextError
        {
            wrapper.Error.With("Url", wrapper.HttpContext.GetUrl());
            return wrapper;
        }

        public static T WithHttpStatusCode<T>(this T wrapper) where T : HttpContextError
        {
            wrapper.Error.With("HttpStatusCode", wrapper.HttpException.GetHttpCode());
            return wrapper;
        }

        public static T WithQueryString<T>(this T wrapper) where T : HttpContextError
        {
            wrapper.Error.With("QueryString", wrapper.HttpContext.GetQueryString());
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
            var form = wrapper.HttpContext.GetFormVariables();
            Glitch.Config.IgnoreContent.Filter("Form", form);
            wrapper.Error.With("Form", form);
            return wrapper;
        }

        public static T WithCookies<T>(this T wrapper) where T:HttpContextError
        {
            var cookies = wrapper.HttpContext.GetCookies();
            Glitch.Config.IgnoreContent.Filter("Cookies", cookies);
            wrapper.Error.With("Cookies", cookies);
            return wrapper;
        }

        public static T WithServerVariables<T>(this T wrapper) where T:HttpContextError
        {
            var serverVariables = wrapper.HttpContext.GetServerVariables();
            Glitch.Config.IgnoreContent.Filter("ServerVariables", serverVariables);
            wrapper.Error.With("ServerVariables", serverVariables);
            return wrapper;
        }

        public static T WithUrlReferer<T>(this T wrapper) where T:HttpContextError
        {
            wrapper.Error.With("UrlReferer", wrapper.HttpContext.GetUrlReferer());
            return wrapper;
        }

    }
}
