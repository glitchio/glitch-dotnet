using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Glitch.Notifier.ConfigElements
{
    public class IgnoreContentElement:ConfigurationElement
    {
        [ConfigurationProperty("dataGroup", IsRequired = true)]
        public string DataGroup
        {
            get
            {
                return (string)this["dataGroup"];
            }
            set
            {
                this["dataGroup"] = value;
            }
        }

        [ConfigurationProperty("keyContains", IsRequired = true)]
        public string KeyContains
        {
            get
            {
                return (string)this["keyContains"];
            }
            set
            {
                this["keyContains"] = value;
            }
        }
    }
}
