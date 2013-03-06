using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ServiceStack.Text;

namespace Glitch.Notifier.Notifications
{
    static class ErrorBatchSerializer
    {
        static ErrorBatchSerializer()
        {
            JsConfig.DateHandler = JsonDateHandler.ISO8601;
            JsConfig.EmitCamelCaseNames = true;
        }

        public static void Serialize(ErrorBatch entity, Stream requestStream)
        {
            var serializer = new JsonSerializer<ErrorBatch>();

            using (var textWriter = new StreamWriter(requestStream))
            {
                serializer.SerializeToWriter(entity, textWriter);
            }
        }

        public static string Serialize(ErrorBatch entity)
        {
            var serializer = new JsonSerializer<ErrorBatch>();
            return serializer.SerializeToString(entity);
        }
    }
}
