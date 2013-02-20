using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Glitch.Notifier
{
    public class Error
    {
        private readonly Exception _exception;

        public Error(string errorMessage)
        {
            if (string.IsNullOrWhiteSpace(errorMessage)) throw new ArgumentException("errorMessage cannot be null or empty");
            ExtraData = new Dictionary<string, object>();
            ErrorMessage = errorMessage;
            //Use UTC or local time?
            OccurredAt = DateTime.Now;
        }

        public Error(Exception exception)
            : this(exception.Message)
        {
            if (exception == null) throw new ArgumentNullException("exception");
            _exception = exception;
            //Get only the stacktrace instead? What if there are inner exceptions?
            ExtraData["StackTrace"] = exception.ToString();
        }


        internal Dictionary<string, object> ExtraData { get; set; }
        internal string ErrorMessage { get; set; }
        internal string Profile { get; set; }
        internal string GroupKey { get; set; }
        internal DateTime OccurredAt { get; set; }

        public Error WithErrorProfile(string profile)
        {
            if (string.IsNullOrWhiteSpace(profile)) throw new ArgumentException("profile cannot be null or empty");
            Profile = profile;
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
            if (value == null) throw new ArgumentNullException("value");

            ExtraData.Add(key, value);
            return this;
        }

        public void Send()
        {
            ApplyDefaultsIfNeeded();
            NotificationSender.Send(this);
        }

        public Task SendAsync()
        {
            ApplyDefaultsIfNeeded();
            return NotificationSender.SendAsync(this);
        }

        private void ApplyDefaultsIfNeeded()
        {
            ApplyGroupKeyDefaultIfNeeded();
            ApplyErrorProfileDefaultIfNeeded();
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
                if (_exception != null)
                {
                    //For the hash, only the error message and the first line of the stacktrace 
                    //are considered.
                    hashSeed = string.Format("{0}|{1}",
                                             _exception.Message,
                                             _exception.GetStackTraceFirstLine());
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
