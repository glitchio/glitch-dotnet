using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glitch.Notifier
{
    public class GlitchConfig
    {
        public GlitchConfig(GlitchConfigSection section)
        {
            if (section == null) return;
            UseApiKey(section.ApiKey);
            if (section.UseHttps) UseHttps();
        }

        public GlitchConfig UseApiKey(string apiKey)
        {
            ApiKey = apiKey;
            return this;
        }

        public GlitchConfig UseHttps()
        {
            IsHttps = true;
            return this;
        }

        public GlitchConfig UseDefaultErrorProfile(string errorProfile)
        {
            ErrorProfile = errorProfile;
            return this;
        }

        public string ApiKey { get; private set; }

        public bool IsHttps { get; private set; }

        private string _errorProfile = "v1.net.default";
        public string ErrorProfile
        {
            get { return _errorProfile; }
            private set { _errorProfile = value; }
        }

        internal string Url
        {
            get
            {
                return Glitch.Config.Scheme + "://api.glitch.io/v1/errors";
            }
        }

        private string Scheme
        {
            get { return IsHttps ? "https" : "http"; }
        }
    }
}
