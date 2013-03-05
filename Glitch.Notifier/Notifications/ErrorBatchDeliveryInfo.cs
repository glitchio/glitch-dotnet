using System;

namespace Glitch.Notifier.Notifications
{
    public class ErrorBatchDeliveryInfo
    {
        public ErrorBatchDeliveryInfo(ErrorBatch errorBatch)
        {
            ErrorBatch = errorBatch;
        }

        public ErrorBatchDeliveryInfo(ErrorBatch errorBatch, Exception exception)
            :this(errorBatch)
        {
            Exception = exception;
        }

        public Exception Exception { get; private set; }
        public ErrorBatch ErrorBatch { get; private set; }
        public bool Succeeded
        {
            get { return Exception == null; }
        }
    }
}