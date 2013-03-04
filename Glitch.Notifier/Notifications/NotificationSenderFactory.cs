using System;

namespace Glitch.Notifier.Notifications
{
    static class NotificationSenderFactory
    {
        private static Func<INotificationSender> _notificationSenderFunc = () => new NotificationSender();

        public static INotificationSender Create()
        {
            return _notificationSenderFunc();
        }

        internal static void SetFactoryFunc(Func<INotificationSender> func)
        {
            _notificationSenderFunc = func;
        }
    }
}