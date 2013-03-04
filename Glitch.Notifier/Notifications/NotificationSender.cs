using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

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
