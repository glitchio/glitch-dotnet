using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glitch.Notifier.ErrorContentFilters
{
    public class ErrorContentFilter:IErrorContentFilter
    {
        internal const string ProtectedText = "***";

        private readonly Dictionary<string, List<string>> _fieldsByDataGroupKey = new Dictionary<string, List<string>>();

        public void Filter(string dataGroupKey, Dictionary<string, string> data)
        {
            List<string> dataFields;
            if(!_fieldsByDataGroupKey.TryGetValue(dataGroupKey, out dataFields))
            {
                return;
            }
            data.Keys.Where(k => dataFields.Any(f => k.IndexOf(f, 
                                            StringComparison.OrdinalIgnoreCase) != -1))
                .ToList()
                .ForEach(k => data[k] = ProtectedText);
        }

        public ErrorContentFilter FromDataGroupWithKeysContaining(string dataGroupKey, 
                                                                    params string[] fields)
        {
           List<string> dataFields;
            if(!_fieldsByDataGroupKey.TryGetValue(dataGroupKey, out dataFields))
            {
                _fieldsByDataGroupKey[dataGroupKey] = dataFields = new List<string>();
            }

            foreach (var field in
                fields.Where(field => dataFields.All(f => 
                    string.Compare(field, f, StringComparison.OrdinalIgnoreCase) != 0)))
            {
                dataFields.Add(field);
            }
            return this;
        }
    }
}
