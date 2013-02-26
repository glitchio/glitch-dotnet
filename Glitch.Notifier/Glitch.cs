using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

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
