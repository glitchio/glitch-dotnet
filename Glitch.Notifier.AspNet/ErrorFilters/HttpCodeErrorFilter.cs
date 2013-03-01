using System;
using System.Collections.Generic;
using System.Net;
using Glitch.Notifier.ErrorFilters;

namespace Glitch.Notifier.AspNet.ErrorFilters
{
    public class HttpCodeErrorFilter:IErrorFilter
    {
        private readonly HttpStatusCode _httpCode;

        public HttpCodeErrorFilter(HttpStatusCode httpCode)
        {
            _httpCode = httpCode;
        }

        public bool Exclude(Error error)
        {
            object code;
            return error.ExtraData.TryGetValue("HttpStatusCode", out code) && code != null &&
                int.Parse(code.ToString()) == (int) _httpCode;
        }
    }
}
