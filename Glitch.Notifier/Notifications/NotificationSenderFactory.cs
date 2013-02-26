using System;

namespace Glitch.Notifier.Notifications
{
    static class NotificationSenderFactory
    {
        private static Func<string,string, INotificationSender> _notificationSenderFunc = (url, apiKey) => new NotificationSender(url, apiKey);

        public static INotificationSender Create(string url, string apiKey)
        {
            return _notificationSenderFunc(url, apiKey);
        }

        internal static void SetFunc(Func<string,string, INotificationSender> func)
        {
            _notificationSenderFunc = func;
        }
    }
}