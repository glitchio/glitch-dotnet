﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glitch.Notifier.AspNet.Utils
{
    public static class ServerUtils
    {
        public static Dictionary<string, string> GetServerInfo()
        {
            return new Dictionary<string, string> { { "Host", Environment.MachineName } };

        }
    }
}
