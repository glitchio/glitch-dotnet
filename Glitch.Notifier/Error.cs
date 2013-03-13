using System;
using System.Collections.Generic;
using System.Configuration;
using System.Runtime.Serialization;
using Glitch.Notifier.Notifications;

namespace Glitch.Notifier
{
    public class Error
    {
        public Error(string errorMessage)
        {
            if (string.IsNullOrEmpty(errorMessage)) throw new ArgumentException("errorMessage cannot be null or empty");
            ErrorData = new Dictionary<string, object>();
            ErrorMessage = errorMessage;
            OccurredAt = DateTime.UtcNow;
        }

        public Error(Exception exception)
            : this(exception.Message)
        {
            if (exception == null) throw new ArgumentNullException("exception");
            Exception = exception;
            //Get only the stacktrace instead? What if there are inner exceptions?
            ErrorData["StackTrace"] = exception.ToString();
        }

        [IgnoreDataMember]
        public Exception Exception { get; private set; }

        public Dictionary<string, object> ErrorData { get; internal set; }
        public string ErrorMessage { get; internal set; }
        public string Profile { get; internal set; }


        public string GroupKey { get; set; }
        public DateTime OccurredAt { get; internal set; }
        public string User { get; internal set; }
        public string Location { get; internal set; }

        public Error WithErrorProfile(string profile)
        {
            if (string.IsNullOrEmpty(profile)) throw new ArgumentException("profile cannot be null or empty");
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
            if (string.IsNullOrEmpty(groupKey)) throw new ArgumentException("groupKey cannot be null or empty");
            GroupKey = groupKey;
            return this;
        }

        public Error With(string key, object value)
        {
            if (string.IsNullOrEmpty(key)) throw new ArgumentException("key cannot be null or empty");
            if (value == null) return this;

            ErrorData[key] = value;
            return this;
        }

        public void Send()
        {
            CheckApiKey();
            if (!Glitch.Config.Notify || Glitch.Config.IgnoreErrors.Exclude(this)) 
                return;
            ApplyDefaultsIfNeeded();
            NotificationSenderFactory.Create().Send(this);
        }

        private void ApplyDefaultsIfNeeded()
        {
            ApplyGroupKeyDefaultIfNeeded();
            ApplyErrorProfileDefaultIfNeeded();
        }

        private static void CheckApiKey()
        {
            if (String.IsNullOrEmpty(Glitch.Config.ApiKey))
                throw new ConfigurationErrorsException("apiKey must be configured");
        }

        private void ApplyErrorProfileDefaultIfNeeded()
        {
            if (string.IsNullOrEmpty(Profile))
            {
                Profile = Glitch.Config.ErrorProfile;
            }
        }

        private void ApplyGroupKeyDefaultIfNeeded()
        {
            //If groupKey is not specified then we do our own grouping. 
            if (string.IsNullOrEmpty(GroupKey))
            {
                GroupKey = Glitch.Config.GroupKeyGenerator(this);
            }
        }
    }
}
