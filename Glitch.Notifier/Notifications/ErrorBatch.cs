using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glitch.Notifier.Notifications
{
    public class ErrorBatch
    {
        public ErrorBatch(Error[] errors)
        {
            Errors = errors;
        }

        public Error[] Errors { get; private set; }
    }
}
