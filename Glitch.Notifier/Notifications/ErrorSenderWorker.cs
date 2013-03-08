using System;
using System.Net;
using System.Threading;

namespace Glitch.Notifier.Notifications
{
    class ErrorSenderWorker : Worker
    {
        private readonly static ErrorSenderWorker _instance = new ErrorSenderWorker();

        public event Action<ErrorBatchDeliveryInfo> OnBatchDelivered = delegate { };

        public static ErrorSenderWorker Instance
        {
            get { return _instance; }
        }

        protected override void DoWork(ManualResetEvent stopEvent)
        {
            var errors = ErrorQueue.Pop(Glitch.Config.NotificationsMaxInterval, stopEvent);
            if (errors.Length == 0) return;
            Send(new ErrorBatch(errors));
        }

        protected virtual void Send(ErrorBatch errors)
        {
            var request = CreateRequest();
            using (var requestStream = request.GetRequestStream())
            {
                ErrorBatchSerializer.Serialize(errors, requestStream);
            }
            try
            {
                using (request.GetResponse())
                {
                    OnBatchDelivered(new ErrorBatchDeliveryInfo(errors));
                }
            }
            catch (WebException ex)
            {
                OnBatchDelivered(new ErrorBatchDeliveryInfo(errors, ex.HandleWebException()));
            }
        }

        private WebRequest CreateRequest()
        {
            var request = HttpWebRequest.Create(Glitch.Config.Url);
            request.SetApiKey(Glitch.Config.ApiKey);
            request.Method = "POST";
            request.ContentType = "application/json";
            return request;
        }
    }
}
