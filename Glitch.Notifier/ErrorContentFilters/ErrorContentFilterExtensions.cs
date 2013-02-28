using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glitch.Notifier.ErrorContentFilters
{
    public static class ErrorContentFilteExtensions
    {
        public static ErrorContentFilter FromCookiesWithFieldsContaining(this ErrorContentFilter filter, string containsMessage)
        {
            //filter.WithFilter(new ContainsErrorMessageFilter(containsMessage));
            return filter;
        }

      
    }
}
