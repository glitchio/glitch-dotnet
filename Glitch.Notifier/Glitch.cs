using System;
using System.Collections.Generic;
using System.Configuration;

namespace Glitch.Notifier
{
    public static class Glitch
    {
        public static readonly GlitchConfig Config;

        public static readonly GlitchErrorFactory Factory = new GlitchErrorFactory();

        public static readonly GlitchNotifications Notifications = new GlitchNotifications();

        static Glitch()
        {
            Config = new GlitchConfig(ConfigurationManager.GetSection("glitch") as GlitchConfigSection);
        }
    }
}
