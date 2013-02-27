using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Glitch.Notifier.ErrorFilters;

namespace Glitch.Notifier
{
    public class GlitchConfig
    {
        public GlitchConfig(GlitchConfigSection section)
        {
            if (section == null) return;
            UseApiKey(section.ApiKey);
            UseHttps(section.UseHttps);
            SendNotifications(section.Notify);
        }

        public GlitchConfig UseApiKey(string apiKey)
        {
            ApiKey = apiKey;
            return this;
        }

        public GlitchConfig UseHttps(bool https)
        {
            IsHttps = https;
            return this;
        }

        public GlitchConfig SendNotifications(bool notify)
        {
            Notify = notify;
            return this;
        }

        public GlitchConfig UseDefaultErrorProfile(string errorProfile)
        {
            ErrorProfile = errorProfile;
            return this;
        }

        public string ApiKey { get; private set; }

        public bool IsHttps { get; private set; }

        private bool _notify = true;
        public bool Notify
        {
            get { return _notify; }
            private set { _notify = value; }
        }

        private string _errorProfile = "glitch/v1.net.default";
       

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

        private readonly ErrorFilterPipeline _errorFilters = new ErrorFilterPipeline();
        public ErrorFilterPipeline ErrorFilters
        {
            get { return _errorFilters; }
        }
    }
}
