﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using Glitch.Notifier.ErrorFilters;

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

        [ConfigurationProperty("notify", DefaultValue = "true")]
        public bool Notify
        {
            get
            {
                return (bool)this["notify"];
            }
            set
            {
                this["notify"] = value;
            }
        }
    }
}
