using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glitch.Notifier.ErrorContentFilters
{
    public class ErrorContentFilter:IErrorContentFilter
    {
        internal const string ProtectedText = "***";

        private readonly List<string> _fields = new List<string>
                                                    {
                                                        "_VIEWSTATE",
                                                        "PASSWORD",
                                                        "ASPXAUTH"
                                                    };
        public void Filter(Dictionary<string, string> data)
        {
            data.Keys.Where(k => _fields.Any(f => k.IndexOf(f, 
                                            StringComparison.OrdinalIgnoreCase) != -1))
                .ToList()
                .ForEach(k => data[k] = ProtectedText);
        }

        public ErrorContentFilter WithFieldsContaining(params string[] fields)
        {
            foreach (var field in 
                fields.Where(field => _fields.All(f => 
                    string.Compare(field, f, StringComparison.OrdinalIgnoreCase) != 0)))
            {
                _fields.Add(field);
            }
            return this;
        }
    }
}
