using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glitch.Notifier.ErrorFilters
{
    public interface IErrorFilter
    {
        bool Exclude(Error error);
    }
}
