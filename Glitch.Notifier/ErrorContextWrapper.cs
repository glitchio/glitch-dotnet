using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glitch.Notifier
{
    public abstract class ErrorContextWrapper
    {
        public Error Error { get; private set; }

        protected ErrorContextWrapper(Error error, string errorProfile)
        {
            Error = error;
            Error.WithErrorProfile(errorProfile);
        }

        public void Send()
        {
            Error.Send();
        }

        public Task SendAsync()
        {
            return Error.SendAsync();
        }
    }
}
