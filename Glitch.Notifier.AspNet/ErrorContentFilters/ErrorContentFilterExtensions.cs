using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Glitch.Notifier.ErrorContentFilters;

namespace Glitch.Notifier.AspNet.ErrorContentFilters
{
    public static class ErrorContentFilterExtensions
    {
        public static ErrorContentFilter
            FromCookiesWithNamesContaining(this ErrorContentFilter filter,
                                            params string[] fields)
        {
            filter.FromDataGroupWithKeysContaining("Cookies", fields);
            return filter;
        }

        public static ErrorContentFilter
           FromServerVariablesWithNamesContaining(this ErrorContentFilter filter,
                                           params string[] fields)
        {
            filter.FromDataGroupWithKeysContaining("ServerVariables", fields);
            return filter;
        }

        public static ErrorContentFilter
          FromFormVariablesWithNamesContaining(this ErrorContentFilter filter,
                                          params string[] fields)
        {
            filter.FromDataGroupWithKeysContaining("Form", fields);
            return filter;
        }

        public static ErrorContentFilter
          FromHttpHeadersWithNamesContaining(this ErrorContentFilter filter,
                                          params string[] fields)
        {
            filter.FromDataGroupWithKeysContaining("HttpHeaders", fields);
            return filter;
        }
    }
}
