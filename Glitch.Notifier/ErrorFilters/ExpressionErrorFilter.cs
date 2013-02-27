using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glitch.Notifier.ErrorFilters
{
    public class ExpressionErrorFilter:IErrorFilter
    {
        private readonly Func<Error, bool> _expression;

        public ExpressionErrorFilter(Func<Error, bool> expression)
        {
            if (expression == null) throw new ArgumentNullException("expression");
            _expression = expression;
        }

        public bool Exclude(Error error)
        {
            return _expression(error);
        }
    }
}
