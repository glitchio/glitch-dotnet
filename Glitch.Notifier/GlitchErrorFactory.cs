using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glitch.Notifier
{
    public class GlitchErrorFactory
    {
        public Error Error(string errorMessage)
        {
            return new Error(errorMessage);
        }

        public Error Error(Exception exception)
        {
            return new Error(exception);
        }
    }
}
