using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Glitch.Notifier.Notifications;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Glitch.Notifier.Tests.CoreSpecifications.IntegrationSpecifications
{
    [TestClass]
    public class When_delivering_an_error_batch
    {
        private readonly TaskCompletionSource<ErrorBatchDeliveryInfo> _errorDeliveryTaskCompletionSource = new TaskCompletionSource<ErrorBatchDeliveryInfo>();

        private Task<ErrorBatchDeliveryInfo> ErrorDeliveryTask
        {
            get { return _errorDeliveryTaskCompletionSource.Task; }
        }

        [TestMethod]
        public void Should_succeed()
        {
            Glitch.Config.WithHttps(false)
                .WithApiKey("4b5edd5ccef34e999ab6a40798f68de1")
                .WithNotificationsMaxBatchSize(2);
            ErrorSenderWorker.Instance.OnBatchDelivered += Instance_OnBatchDelivered;

            Glitch.Factory.Error("Test error")
                          .Send();
            Glitch.Factory.Error("Test error 2")
                          .Send();

            ErrorDeliveryTask.Wait(TimeSpan.FromMinutes(1));
            var info = ErrorDeliveryTask.Result;
            Assert.AreEqual(2, info.ErrorBatch.Errors.Length);
            Assert.IsNull(info.Exception);
            Assert.IsTrue(info.Succeeded);
            ErrorSenderWorker.Instance.Stop(TimeSpan.Zero);
        }

        void Instance_OnBatchDelivered(ErrorBatchDeliveryInfo info)
        {
            _errorDeliveryTaskCompletionSource.SetResult(info);
        }
    }
}
