using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Glitch.Notifier
{
    static class NotificationSender
    {
        public static void Send(Error entity)
        {
            var request = CreateRequest();
            using (var requestStream = request.GetRequestStream())
            {
                WriteError(entity, requestStream);
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

        //much easier with async/await !! but we're targeting .NET 4.0
        public static Task SendAsync(Error entity)
        {
            var request = CreateRequest();
            var taskCompletionSource = new TaskCompletionSource<bool>();

            Task.Factory.FromAsync<Stream>(request.BeginGetRequestStream,
              request.EndGetRequestStream, null, TaskCreationOptions.None)
              .ContinueWith(task =>
              {
                  if (HasError(task, taskCompletionSource)) return;

                  using (var requestStream = task.Result)
                  {
                      WriteError(entity, requestStream);
                  }
              })
              .ContinueWith(task =>
              {
                  if (HasError(task, taskCompletionSource)) return;

                  Task.Factory.FromAsync<WebResponse>(request.BeginGetResponse,
                    request.EndGetResponse, null, TaskCreationOptions.None)
                    .ContinueWith(task2 =>
                    {
                        if (HasError(task2, taskCompletionSource)) return;
                       
                        WebResponse webResponse = null;
                        try
                        {

                            webResponse = task2.Result;
                            taskCompletionSource.SetResult(true);
                        }
                        catch(WebException ex)
                        {
                            taskCompletionSource.SetException(ex.WrapException());
                        }
                        finally
                        {
                            if (webResponse != null)
                                webResponse.Close();
                        }
                    });
              });
            return taskCompletionSource.Task;
        }

        private static bool HasError(Task task, TaskCompletionSource<bool> taskCompletionSource)
        {
            if (task.IsFaulted && task.Exception != null)
            {
                taskCompletionSource.SetException(task.Exception);
                return true;
            }
            return false;
        }

        private static void WriteError(Error entity, Stream requestStream)
        {
            using (var textWriter = new StreamWriter(requestStream))
            {
                new JsonSerializer
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                }.Serialize(textWriter, entity);
            }
        }

        private static WebRequest CreateRequest()
        {
            var request = HttpWebRequest.Create(Glitch.Config.Url);
            request.SetApiKey();
            request.HandleProxySettings();
            request.Method = "POST";
            request.ContentType = "application/json";
            return request;
        }
    }
}
