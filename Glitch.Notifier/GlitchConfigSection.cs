using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Glitch.Notifier
{
    public class GlitchConfigSection : ConfigurationSection
    {
        [ConfigurationProperty("apiKey", DefaultValue = "", IsRequired = true)]
        public string ApiKey
        {
            get
            {
                return (string)this["apiKey"];
            }
            set
            {
                this["apiKey"] = value;
            }
        }

        [ConfigurationProperty("useHttps", DefaultValue = "false")]
        public bool UseHttps
        {
            get
            {
                return (bool)this["useHttps"];
            }
            set
            {
                this["useHttps"] = value;
            }
        }
    }
}
