using System;
using System.Configuration;
using System.Text.RegularExpressions;
using Glitch.Notifier.ErrorFilters;

namespace Glitch.Notifier.ConfigElements
{
    public class IgnoreErrorsElement : ConfigurationElement
    {
        [ConfigurationProperty("key", IsRequired = true)]
        public string FilterKey
        {
            get
            {
                return (string)this["key"];
            }
            set
            {
                this["key"] = value;
            }
        }

        [ConfigurationProperty("expression", IsRequired = true)]
        public string Expression
        {
            get
            {
                return (string)this["expression"];
            }
            set
            {
                this["expression"] = value;
            }
        }

        public IErrorFilter CreateFilter()
        {
            try
            {
                new Regex(Expression);
            }
            catch (Exception ex)
            {
                throw new ConfigurationErrorsException("Invalid regular expression (ignoreErrors tag expression attribute", ex);
            }

            if (string.Compare(FilterKey, "ExceptionType", StringComparison.OrdinalIgnoreCase) == 0)
            {
                return new ExpressionErrorFilter(error =>
                                                     {
                                                         if (error.Exception == null) return false;
                                                         return
                                                             new Regex(Expression).IsMatch(
                                                                 error.Exception.GetType().ToString());
                                                     });
            }
            if (string.Compare(FilterKey, "ErrorMessage", StringComparison.OrdinalIgnoreCase) == 0)
            {
                return new ExpressionErrorFilter(error => new Regex(Expression).IsMatch(
                                                                 error.ErrorMessage));
            }
            return new ExpressionErrorFilter(error =>
                                                 {
                                                     object value;
                                                     if (!error.ExtraData.TryGetValue(FilterKey, out value) || value == null || value.GetType() != typeof(string))
                                                         return false;
                                                     return new Regex(Expression).IsMatch(value.ToString());
                                                 });
        }
    }
}
