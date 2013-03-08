using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Glitch.Notifier.Notifications
{
    abstract class Worker
    {
        private readonly object _lock = new object();
        private Thread _thread;
        private readonly ManualResetEvent _stopEvent = new ManualResetEvent(false);
        private readonly ManualResetEvent _shutdownEvent = new ManualResetEvent(false);

        public void Start()
        {
            lock (_lock)
            {
                if (_thread != null) return;

                _thread = new Thread(Run);
                _thread.Start();
            }
        }

        private void Run()
        {
            try
            {
                while (!_stopEvent.WaitOne(0))
                {
                    DoWork(_stopEvent);
                }
                DoWork(_stopEvent);
            }
            catch
            {

            }
            finally
            {
                _shutdownEvent.Set();
            }
        }

        protected abstract void DoWork(ManualResetEvent stopEvent);

        public bool Stop(TimeSpan timeout)
        {
            _stopEvent.Set();
            return _shutdownEvent.WaitOne(timeout);
        }
    }
}
