using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Glitch.Notifier.ErrorFilters;

namespace Glitch.Notifier.AspNet.ErrorFilters
{
    public class ContainsUrlFragmentErrorFilter:IErrorFilter
    {
        private readonly string _urlFragment;

        public ContainsUrlFragmentErrorFilter(string urlFragment)
        {
            if (urlFragment == null) throw new ArgumentNullException("urlFragment");
            _urlFragment = urlFragment;
        }

        public bool Exclude(Error error)
        {
            object url;
            return error.ExtraData.TryGetValue("Url", out url) && url != null &&
                url.ToString().IndexOf(_urlFragment, StringComparison.OrdinalIgnoreCase)!=-1;
        }
    }
}
