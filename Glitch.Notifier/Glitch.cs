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

        public static Error Notify(string errorMessage)
        {
            return new Error(errorMessage);
        }

        public static Error Notify(Exception exception)
        {
            return new Error(exception);
        }
        
    }
}
