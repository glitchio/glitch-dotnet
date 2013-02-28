using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Glitch.Notifier.ErrorFilters
{
    public class ErrorFilter:IErrorFilter
    {
        private readonly List<IErrorFilter> _filters = new List<IErrorFilter>();

        public ErrorFilter WithFilter(IErrorFilter filter)
        {
            if (filter == null) throw new ArgumentNullException("filter");
            _filters.Add(filter);
            return this;
        }
        
        public bool Exclude(Error error)
        {
            return _filters.Any(f => f.Exclude(error));
        }
    }
}
