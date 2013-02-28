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
        public static ErrorFilterPipeline WithHttpCode(this ErrorFilterPipeline pipeline, HttpStatusCode statusCode)
        {
            pipeline.WithFilter(new HttpCodeErrorFilter(statusCode));
            return pipeline;
        }

        public static ErrorFilterPipeline WithUrlFragment(this ErrorFilterPipeline pipeline, string urlFragment)
        {
            pipeline.WithFilter(new ContainsUrlFragmentErrorFilter(urlFragment));
            return pipeline;
        }

        public static ErrorFilterPipeline WithUrlMatching(this ErrorFilterPipeline pipeline, string expression)
        {
            pipeline.WithFilter(new RegexUrlErrorFilter(expression));
            return pipeline;
        }
    }
}
