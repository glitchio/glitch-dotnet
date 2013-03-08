using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Glitch.Notifier.Notifications;

namespace Glitch.Notifier
{
    public class GlitchNotifications
    {
        public GlitchNotifications()
        {
            ErrorSenderWorker.Instance.OnBatchDelivered += info => OnBatchDelivered(info);
        }

        public bool Stop(TimeSpan timeout)
        {
            return ErrorSenderWorker.Instance.Stop(timeout);
        }

        public event Action<ErrorBatchDeliveryInfo> OnBatchDelivered = delegate { };
    }
}
