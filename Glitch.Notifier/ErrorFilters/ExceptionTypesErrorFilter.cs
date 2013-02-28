using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glitch.Notifier.ErrorFilters
{
    public class ExceptionTypesErrorFilter:IErrorFilter
    {
        private readonly Type[] _types;

        public ExceptionTypesErrorFilter(params Type[] types)
        {
            if (types == null) throw new ArgumentNullException("types");
            _types = types;
        }

        public bool Exclude(Error error)
        {
            return error.Exception != null && _types.Any(t => error.Exception.GetType() == t);
        }
    }
}
