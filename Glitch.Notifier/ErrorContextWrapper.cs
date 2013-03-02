using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glitch.Notifier
{
    public abstract class ErrorContextWrapper
    {
        public Error Error { get; private set; }

        protected ErrorContextWrapper(Error error)
        {
            Error = error;
        }

        public void Send()
        {
            Error.Send();
        }
    }
}
