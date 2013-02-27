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
        public static ErrorFilterPipeline WithHttpCodeFilter(this ErrorFilterPipeline pipeline, HttpStatusCode statusCode)
        {
            pipeline.WithFilter(new HttpCodeErrorFilter(statusCode));
            return pipeline;
        }
    }
}
