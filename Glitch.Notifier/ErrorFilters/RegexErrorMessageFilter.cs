using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Glitch.Notifier.ErrorFilters
{
    public class RegexErrorMessageFilter : IErrorFilter
    {
        private readonly Regex _regex;
        public RegexErrorMessageFilter(string expression)
        {
            if (expression == null) throw new ArgumentNullException("expression");
            _regex = new Regex(expression);
        }

        public bool Exclude(Error error)
        {
            return error.ErrorMessage != null && _regex.IsMatch(error.ErrorMessage);
        }
    }
}
