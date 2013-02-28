using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using Glitch.Notifier.ErrorFilters;

namespace Glitch.Notifier.AspNet.ErrorFilters
{
    public static class ErrorFilterPipelineExtensions
    {
        public static ErrorFilter WithHttpCode(this ErrorFilter pipeline, HttpStatusCode statusCode)
        {
            pipeline.WithFilter(new HttpCodeErrorFilter(statusCode));
            return pipeline;
        }

        public static ErrorFilter WithUrlFragment(this ErrorFilter pipeline, string urlFragment)
        {
            pipeline.WithFilter(new ContainsUrlFragmentErrorFilter(urlFragment));
            return pipeline;
        }

        public static ErrorFilter WithUrlMatching(this ErrorFilter pipeline, string expression)
        {
            pipeline.WithFilter(new RegexUrlErrorFilter(expression));
            return pipeline;
        }
    }
}
