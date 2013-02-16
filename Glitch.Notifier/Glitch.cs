using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Glitch.Notifier
{
    public static class Glitch
    {
        public static readonly GlitchConfig Config = new GlitchConfig();
        
        //extraData... Use a dynamic param instead of a Dictionary ?
        public static void Notify(string errorMessage, Dictionary<string, object> extraData = null,
                                  string errorProfile = null, string groupKey = null)
        {
            Notify(ErrorDto.Create(errorMessage, extraData, GetErrorProfile(errorProfile), groupKey));
        }

        public static Task NotifyAsync(string errorMessage, Dictionary<string, object> extraData = null,
                                  string errorProfile = null, string groupKey = null)
        {
            return NotifyAsync(ErrorDto.Create(errorMessage, extraData, GetErrorProfile(errorProfile), groupKey));
        }

        public static void Notify(Exception exception, Dictionary<string, object> extraData = null,
                                  string errorProfile = null, string groupKey = null)
        {

            Notify(ErrorDto.Create(exception, extraData, GetErrorProfile(errorProfile), groupKey));
        }

        public static Task NotifyAsync(Exception exception, Dictionary<string, object> extraData = null,
                                  string errorProfile = null, string groupKey = null)
        {
            return NotifyAsync(ErrorDto.Create(exception, extraData, GetErrorProfile(errorProfile), groupKey));
        }

        private static void Notify(ErrorDto errorDto)
        {
            var request = (HttpWebRequest)HttpWebRequest.Create(Url);
            SetApiKey(request);
            request.Method = "POST";
            request.ContentType = "application/json";
            using (var requestStream = request.GetRequestStream())
            using (var textWriter = new StreamWriter(requestStream))
            {
                new JsonSerializer
                    {
                        ContractResolver = new CamelCasePropertyNamesContractResolver()
                    }
                    .Serialize(textWriter, errorDto);
            }
            try
            {
                using (request.GetResponse())
                {

                }
            }
            catch (WebException ex)
            {
                ex.HandleWebException();

            }
        }

        private static Task NotifyAsync(ErrorDto errorDto)
        {
            throw new NotImplementedException();
        }

        private static void SetApiKey(HttpWebRequest request)
        {
            var apiKey = Config.ApiKey;
            if (string.IsNullOrWhiteSpace(apiKey))
                throw new ConfigurationErrorsException("api Key must be configured");
            request.Headers.Add(HttpRequestHeader.Authorization, "key " + apiKey);
        }

        private static string Url
        {
            get { return Config.Scheme + "://api.glitch.io/v1/errors"; }
        }

        private static string GetErrorProfile(string errorProfile)
        {
            return string.IsNullOrWhiteSpace(errorProfile) 
                ? Config.ErrorProfile : errorProfile;
        }
    }
}
