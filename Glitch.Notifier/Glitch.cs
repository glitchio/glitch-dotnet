using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;

namespace Glitch.Notifier
{
    public static class Glitch
    {
        public static readonly GlitchConfig Config;

        public static readonly GlitchErrorFactory Factory = new GlitchErrorFactory();

        static Glitch()
        {
            Config = new GlitchConfig(ConfigurationManager.GetSection("glitch") as GlitchConfigSection);
        }
    }
}
