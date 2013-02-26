using System.Threading.Tasks;

namespace Glitch.Notifier.Notifications
{
    internal interface INotificationSender
    {
        void Send(Error entity);
        Task SendAsync(Error entity);
    }
}