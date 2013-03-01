
namespace Glitch.Notifier.Notifications
{
    internal interface INotificationSender
    {
        void Send(Error entity);
    }
}