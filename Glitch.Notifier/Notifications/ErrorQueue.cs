using System;
using System.Collections.Generic;
using System.Threading;

namespace Glitch.Notifier.Notifications
{
    internal static class ErrorQueue
    {
        private static readonly Queue<Error> Queue = new Queue<Error>();
        private static readonly ManualResetEvent HasEnoughItemsEvent = new ManualResetEvent(false);
        public static event Action<Error> OnErrorPushing = delegate { };

        private static int MaximumBatchSize
        {
            get
            {
                return Glitch.Config.NotificationsMaxBatchSize;
            }
        }

        public static void Push(Error item)
        {
            OnErrorPushing(item);
            lock (Queue)
            {
                Queue.Enqueue(item);
                if (Queue.Count >= MaximumBatchSize)
                {
                    HasEnoughItemsEvent.Set();
                }
            }
        }

        public static Error[] Pop(TimeSpan timeout, ManualResetEvent cancelEvent)
        {
            WaitHandle.WaitAny(new WaitHandle[] { HasEnoughItemsEvent, cancelEvent }, timeout);
            return Pop();
        }

        private static Error[] Pop()
        {
            lock (Queue)
            {
                var items = new List<Error>();
                var count = MaximumBatchSize > Queue.Count ? Queue.Count : MaximumBatchSize;
                for (var i = 0; i < count; i++)
                {
                    items.Add(Queue.Dequeue());
                }
                if (Queue.Count < MaximumBatchSize) HasEnoughItemsEvent.Reset();
                return items.ToArray();
            }
        }

        internal static void Clear()
        {
            lock (Queue)
            {
                Queue.Clear();
                HasEnoughItemsEvent.Reset();
            }
        }
    }
}
