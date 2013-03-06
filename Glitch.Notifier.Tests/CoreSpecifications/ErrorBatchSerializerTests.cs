using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using Glitch.Notifier.Notifications;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceStack.Text;

namespace Glitch.Notifier.Tests.CoreSpecifications
{
    [TestClass]
    public class ErrorBatchSerializerTests
    {
        [TestMethod]
        public void Should_serialize()
        {
            var error = Glitch.Factory.Error(new InvalidOperationException("test"));
            var errorBatch = new ErrorBatch(new[] {error});

            var json = ErrorBatchSerializer.Serialize(errorBatch);

            Assert.IsTrue(json.StartsWith("{\"errors\":[{\"extraData\":{\"StackTrace\":\"System.InvalidOperationException: test\"},\"errorMessage\":\"test\",\"occurredAt\":\"201"));
        }
    }
}
