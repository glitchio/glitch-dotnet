using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glitch.Notifier.ErrorFilters
{
    public static class ErrorFilterPipelineExtensions
    {
        public static ErrorFilterPipeline WithErrorMessageContaining(this ErrorFilterPipeline pipeline, string containsMessage)
        {
            pipeline.WithFilter(new ContainsErrorMessageFilter(containsMessage));
            return pipeline;
        }

        public static ErrorFilterPipeline WithErrorMessageMatching(this ErrorFilterPipeline pipeline, string expression)
        {
            pipeline.WithFilter(new RegexErrorMessageFilter(expression));
            return pipeline;
        }

        public static ErrorFilterPipeline WithExceptionTypes(this ErrorFilterPipeline pipeline, Type type)
        {
            pipeline.WithFilter(new ExceptionTypesErrorFilter(type));
            return pipeline;
        }

        public static ErrorFilterPipeline Where(this ErrorFilterPipeline pipeline, Func<Error, bool> expression)
        {
            pipeline.WithFilter(new ExpressionErrorFilter(expression));
            return pipeline;
        }
    }
}
