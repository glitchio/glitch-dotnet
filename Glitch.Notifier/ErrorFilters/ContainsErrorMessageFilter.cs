using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glitch.Notifier.ErrorFilters
{
    public class ContainsErrorMessageFilter: IErrorFilter
    {
        private readonly string _message;

        public ContainsErrorMessageFilter(string message)
        {
            if (message == null) throw new ArgumentNullException("message");
            _message = message;
        }

        public bool Exclude(Error error)
        {
            return error.ErrorMessage != null &&
                   error.ErrorMessage.IndexOf(_message, StringComparison.OrdinalIgnoreCase) != -1;
        }
    }
}
