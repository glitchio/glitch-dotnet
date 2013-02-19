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
    //Static or instance? 
    public static class Glitch
    {
        public static readonly GlitchConfig Config = new GlitchConfig();

        public static Error Error(string errorMessage)
        {
            return new Error(errorMessage);
        }

        public static Error Error(Exception exception, Dictionary<string, object> extraData, string errorProfile, string groupKey)
        {
            return new Error(exception);
        }
        
    }
}
