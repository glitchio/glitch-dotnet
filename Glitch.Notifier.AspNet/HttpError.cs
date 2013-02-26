using System;
using System.Web;

namespace Glitch.Notifier.AspNet
{
    public class HttpError:ErrorWrapper
    {
        public HttpContextBase HttpContext { get; private set; }

        public HttpError(Exception exception, HttpContextBase httpContext)
            : base(new Error(exception))
        {
            HttpContext = httpContext;
            Error.WithLocation(HttpContext.GetUrl());
        }
    }

    public static class HttpContextErrorExtensions
    {

        public static T WithContextData<T>(this T wrapper) where T:HttpError
        {
            return
                    wrapper.WithCurrentUser()
                    .WithHttpHeaders()
                    .WithHttpMethod()
                    .WithUrl()
                    .WithQueryString()
                    .WithServerInfo()
                    .WithClientInfo()
                    .WithStatusCode();
        }

        public static T WithHttpHeaders<T>(this T wrapper) where T:HttpError
        {
            wrapper.Error.With("HttpHeaders", wrapper.HttpContext.GetHttpHeaders());
            return wrapper;
        }

        public static T WithUrl<T>(this T wrapper) where T : HttpError
        {
            wrapper.Error.With("Url", wrapper.HttpContext.GetUrl());
            return wrapper;
        }

        public static T WithStatusCode<T>(this T wrapper) where T : HttpError
        {
            wrapper.Error.With("HttpStatusCode", wrapper.HttpContext.GetStatusCode());
            return wrapper;
        }

        public static T WithQueryString<T>(this T wrapper) where T : HttpError
        {
            wrapper.Error.With("QueryString", wrapper.HttpContext.GetHttpHeaders());
            return wrapper;
        }

        public static T WithCurrentUser<T>(this T wrapper) where T : HttpError
        {
            wrapper.Error.WithUser(wrapper.HttpContext.GetCurrentUser());
            return wrapper;
        }

        public static T WithHttpMethod<T>(this T wrapper) where T : HttpError
        {
            wrapper.Error.With("HttpMethod", wrapper.HttpContext.GetHttpMethod());
            return wrapper;
        }

        public static T WithErrorProfile<T>(this T wrapper, string errorProfile) where T : HttpError
        {
            wrapper.Error.WithErrorProfile(errorProfile);
            return wrapper;
        }

        public static T WithServerInfo<T>(this T wrapper) where T: HttpError
        {
            wrapper.Error.With("ServerInfo", Utils.GetServerInfo());
            return wrapper;
        }

        public static T WithClientInfo<T>(this T wrapper) where T:HttpError
        {
            wrapper.Error.With("ClientInfo", wrapper.HttpContext.GetClientInfo());
            return wrapper;
        }
    }
}
