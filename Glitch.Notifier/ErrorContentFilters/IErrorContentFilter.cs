using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glitch.Notifier.ErrorContentFilters
{
    public interface IErrorContentFilter
    {
        void Filter(Dictionary<string, string> data);
    }
}
