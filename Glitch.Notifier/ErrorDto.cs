using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glitch.Notifier
{
    internal class ErrorDto
    {
        public static ErrorDto Create(string errorMessage, Dictionary<string, object> extraData, string errorProfile, string groupKey)
        {
            var errorDto = new ErrorDto
            {
                ErrorMessage = errorMessage,
                ExtraData = extraData,
                Profile = errorProfile,
                GroupKey = string.IsNullOrEmpty(groupKey)
                                    ? Crypto.Hash(errorMessage) : groupKey,
                OccurredAt = DateTime.UtcNow //Use utc or local?
            };
            return errorDto;
        }

        public static ErrorDto Create(Exception exception, Dictionary<string, object> extraData, string errorProfile, string groupKey)
        {
            //clone before modifying
            var extraDataCopy = extraData == null ? new Dictionary<string, object>()
                                                  : extraData.ToDictionary(i => i.Key, i => i.Value);
            //Get only the stacktrace instead? What if there are inner exceptions?
            extraData["StackTrace"] = exception.ToString();

            var groupKeyCopy = groupKey;
            if (string.IsNullOrWhiteSpace(groupKeyCopy))
            {
                //If groupKey is not specified then we do our own grouping
                //For the hash, only the error message and the first line of the stacktrace 
                //are considered.
                var hashSeed = string.Format("{0}|{1}",
                                             exception.Message,
                                             exception.GetStackTraceFirstLine());
                groupKeyCopy = Crypto.Hash(hashSeed);
            }
            return Create(exception.Message, extraDataCopy, errorProfile, groupKeyCopy);
        }

        public string ErrorMessage { get; private set; }
        public Dictionary<string, object> ExtraData { get; private set; }
        public string Profile { get; private set; }
        public string GroupKey { get; private set; }
        public DateTime OccurredAt { get; private set; }
    }
}
