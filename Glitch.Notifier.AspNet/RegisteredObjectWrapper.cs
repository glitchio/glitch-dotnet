using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Hosting;
using Glitch.Notifier.Notifications;

namespace Glitch.Notifier.AspNet
{

    /// http://haacked.com/archive/2011/10/16/the-dangers-of-implementing-recurring-background-tasks-in-asp-net.aspx
    class RegisteredObjectWrapper : IRegisteredObject
    {
        private readonly Worker _worker;

        public RegisteredObjectWrapper(Worker worker)
        {
            _worker = worker;
        }

        public void Stop(bool immediate)
        {
            _worker.Stop(TimeSpan.FromSeconds(immediate ? 0 : 30));
            HostingEnvironment.UnregisterObject(this);
        }

        public void Register()
        {
            HostingEnvironment.RegisterObject(this);
        }
    }
}
