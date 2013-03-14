
using System;

namespace Glitch.Notifier.Notifications
{
    internal interface INotificationSender
    {
        void Send(Error error);
    }
}