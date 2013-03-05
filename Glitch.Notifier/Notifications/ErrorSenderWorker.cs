using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Glitch.Notifier.Notifications
{
    public class ErrorSenderWorker : Worker
    {
        private readonly static ErrorSenderWorker _instance = new ErrorSenderWorker();

        internal ErrorSenderWorker()
        {
            
        }

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
                WriteError(errors, requestStream);
            }
            try
            {
                using (request.GetResponse()) { }
            }
            catch (WebException ex)
            {
                ex.HandleWebException();

            }
        }

        private static void WriteError(ErrorBatch entity, Stream requestStream)
        {
            using (var textWriter = new StreamWriter(requestStream))
            {
                new JsonSerializer
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                }.Serialize(textWriter, entity);
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
