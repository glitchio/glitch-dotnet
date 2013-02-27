using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Glitch.Notifier.ErrorFilters;

namespace Glitch.Notifier.AspNet.ErrorFilters
{
    public class RegexUrlErrorFilter:IErrorFilter
    {
        private readonly Regex _regex;

        public RegexUrlErrorFilter(string expression)
        {
            if (expression == null) throw new ArgumentNullException("expression");
           _regex = new Regex(expression);
        }

        public bool Exclude(Error error)
        {
            object url;
            return error.ExtraData.TryGetValue("Url", out url) && url != null &&
                _regex.IsMatch(url.ToString());
        }
    }
}
