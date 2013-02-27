using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glitch.Notifier.ErrorFilters
{
    public static class ErrorFilterPipelineExtensions
    {
        public static ErrorFilterPipeline WithContainsErrorMessageFilter(this ErrorFilterPipeline pipeline, string containsMessage)
        {
            pipeline.WithFilter(new ContainsErrorMessageFilter(containsMessage));
            return pipeline;
        }

        public static ErrorFilterPipeline WithRegexErrorMessageFilter(this ErrorFilterPipeline pipeline, string expression)
        {
            pipeline.WithFilter(new RegexErrorMessageFilter(expression));
            return pipeline;
        }

        public static ErrorFilterPipeline WithExceptionType(this ErrorFilterPipeline pipeline, Type type)
        {
            pipeline.WithFilter(new ExceptionTypeErrorFilter(type));
            return pipeline;
        }
    }
}
