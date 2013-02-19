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
    static class Notifier
    {
        public static void Notify(Error entity)
        {
            var request = CreateRequest();
            using (var requestStream = request.GetRequestStream())
            using (var textWriter = new StreamWriter(requestStream))
            {
                new JsonSerializer
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                }.Serialize(textWriter, entity);
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

        public static Task NotifyAsync(Error entity)
        {
           throw new NotImplementedException();
        }

        private static WebRequest CreateRequest()
        {
            var request = HttpWebRequest.Create(Glitch.Config.Url);
            request.SetApiKey();
            request.Method = "POST";
            request.ContentType = "application/json";
            return request;
        }
    }
}
