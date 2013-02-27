using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glitch.Notifier.ErrorFilters
{
    public class ExceptionTypeErrorFilter:IErrorFilter
    {
        private readonly Type _type;

        public ExceptionTypeErrorFilter(Type type)
        {
            if (type == null) throw new ArgumentNullException("type");
            _type = type;
        }

        public bool Exclude(Error error)
        {
            return error.Exception != null && error.Exception.GetType() == _type;
        }
    }
}
