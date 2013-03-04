using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Glitch.Notifier.Notifications
{
    internal static class ErrorQueue
    {
        private static readonly Queue<Error> Queue = new Queue<Error>();
        private static readonly ReaderWriterLockSlim LockSlim = new ReaderWriterLockSlim();
        private static readonly ManualResetEvent HasEnoughItemsEvent = new ManualResetEvent(false);

        private static int MaximumBatchSize
        {
            get
            {
                return Glitch.Config.NotificationsMaxBatchSize;
            }
        }

        public static void Push(Error item)
        {
            LockSlim.EnterWriteLock();
            try
            {
                Queue.Enqueue(item);
                if (Queue.Count >= MaximumBatchSize)
                {
                    HasEnoughItemsEvent.Set();
                }
            }
            finally
            {
                LockSlim.ExitWriteLock();
            }
        }

        public static Error[] Pop(TimeSpan timeout, ManualResetEvent cancelEvent)
        {
            LockSlim.EnterUpgradeableReadLock();
            try
            {
                if (Queue.Count < MaximumBatchSize)
                {
                    WaitHandle.WaitAny(new WaitHandle[] { HasEnoughItemsEvent, cancelEvent }, timeout);
                }
                return Pop();
            }
            finally
            {
                LockSlim.EnterUpgradeableReadLock();
                HasEnoughItemsEvent.Reset();
            }
        }

        private static Error[] Pop()
        {
            LockSlim.EnterWriteLock();
            try
            {
                return Queue.Take(MaximumBatchSize).ToArray();
            }
            finally
            {
                LockSlim.ExitWriteLock();
            }
        }

    }
}
