using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glitch.Notifier.ErrorFilters
{
    public static class ErrorFilterExtensions
    {
        public static ErrorFilter WithErrorMessageContaining(this ErrorFilter pipeline, string containsMessage)
        {
            pipeline.WithFilter(new ContainsErrorMessageFilter(containsMessage));
            return pipeline;
        }

        public static ErrorFilter WithErrorMessageMatching(this ErrorFilter pipeline, string expression)
        {
            pipeline.WithFilter(new RegexErrorMessageFilter(expression));
            return pipeline;
        }

        public static ErrorFilter WithExceptionTypes(this ErrorFilter pipeline, Type type)
        {
            pipeline.WithFilter(new ExceptionTypesErrorFilter(type));
            return pipeline;
        }

        public static ErrorFilter Where(this ErrorFilter pipeline, Func<Error, bool> expression)
        {
            pipeline.WithFilter(new ExpressionErrorFilter(expression));
            return pipeline;
        }
    }
}
