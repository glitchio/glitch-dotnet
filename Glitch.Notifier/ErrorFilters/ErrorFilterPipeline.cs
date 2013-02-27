using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glitch.Notifier.ErrorFilters
{
    public class ErrorFilterPipeline:IErrorFilter
    {
        private readonly List<IErrorFilter> _filters = new List<IErrorFilter>();

        public ErrorFilterPipeline WithFilter(IErrorFilter filter)
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
