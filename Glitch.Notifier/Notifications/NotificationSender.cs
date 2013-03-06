using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;

namespace Glitch.Notifier.Notifications
{
    class NotificationSender : INotificationSender
    {
        public void Send(Error entity)
        {
           ErrorSenderWorker.Instance.Start();
           ErrorQueue.Push(entity);
        }
    }
}
