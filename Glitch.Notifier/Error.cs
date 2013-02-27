using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;
using Glitch.Notifier.Notifications;
using Newtonsoft.Json;

namespace Glitch.Notifier
{
    public class Error
    {
        public Error(string errorMessage)
        {
            if (string.IsNullOrWhiteSpace(errorMessage)) throw new ArgumentException("errorMessage cannot be null or empty");
            ExtraData = new Dictionary<string, object>();
            ErrorMessage = errorMessage;
            OccurredAt = DateTime.UtcNow;
        }

        public Error(Exception exception)
            : this(exception.Message)
        {
            if (exception == null) throw new ArgumentNullException("exception");
            Exception = exception;
            //Get only the stacktrace instead? What if there are inner exceptions?
            ExtraData["StackTrace"] = exception.ToString();
        }

        [JsonIgnore]
        public Exception Exception { get; private set; }
        
        public Dictionary<string, object> ExtraData { get; internal set; }
        public string ErrorMessage { get; internal set; }
        public string Profile { get; internal set; }
        public string GroupKey { get; internal set; }
        public DateTime OccurredAt { get; internal set; }
        public string User { get; internal set; }
        public string Location { get; internal set; }

        public Error WithErrorProfile(string profile)
        {
            if (string.IsNullOrWhiteSpace(profile)) throw new ArgumentException("profile cannot be null or empty");
            Profile = profile;
            return this;
        }

        public Error WithUser(string user)
        {
            User = user;
            return this;
        }

        public Error WithLocation(string location)
        {
            Location = location;
            return this;
        }

        public Error WithGroupKey(string groupKey)
        {
            if (string.IsNullOrWhiteSpace(groupKey)) throw new ArgumentException("groupKey cannot be null or empty");
            GroupKey = groupKey;
            return this;
        }

        public Error With(string key, object value)
        {
            if (string.IsNullOrWhiteSpace(key)) throw new ArgumentException("key cannot be null or empty");
            if (value == null) return this;

            ExtraData[key] = value;
            return this;
        }

        public void Send()
        {
            if (!Glitch.Config.Notify) return;
            CheckApiKey();
            ApplyDefaultsIfNeeded();
            NotificationSenderFactory.Create(Glitch.Config.Url, Glitch.Config.ApiKey).Send(this);
        }

        public Task SendAsync()
        {
            if(!Glitch.Config.Notify)
            {
                var ts = new TaskCompletionSource<bool>();
                ts.SetResult(true);
                return ts.Task;
            }
            CheckApiKey();
            ApplyDefaultsIfNeeded();
            return NotificationSenderFactory.Create(Glitch.Config.Url, Glitch.Config.ApiKey).SendAsync(this);
        }

        private void ApplyDefaultsIfNeeded()
        {
            ApplyGroupKeyDefaultIfNeeded();
            ApplyErrorProfileDefaultIfNeeded();
        }

        private static void CheckApiKey()
        {
            if (String.IsNullOrWhiteSpace(Glitch.Config.ApiKey))
                throw new ConfigurationErrorsException("apiKey must be configured");
        }

        private void ApplyErrorProfileDefaultIfNeeded()
        {
            if (string.IsNullOrWhiteSpace(Profile))
            {
                Profile = Glitch.Config.ErrorProfile;
            }
        }

        private void ApplyGroupKeyDefaultIfNeeded()
        {
            //If groupKey is not specified then we do our own grouping. 
            if (string.IsNullOrWhiteSpace(GroupKey))
            {
                string hashSeed;
                //If an exception was provided, we have more info to create a groupKey
                if (Exception != null)
                {
                    //For the hash, only the error message and the first line of the stacktrace 
                    //are considered.
                    hashSeed = string.Format("{0}|{1}",
                                             Exception.Message,
                                             Exception.GetStackTraceFirstLine());
                }
                else
                {
                    hashSeed = ErrorMessage;
                }

                GroupKey = Crypto.Hash(hashSeed);
            }
        }
    }
}
