using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Glitch.Notifier.Notifications;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Glitch.Notifier.Tests.CoreSpecifications
{
    [TestClass]
    public class ErrorSenderWorkerSpecifications
    {
        private TestErrorSenderWorker _testErrorSenderWorker ;

        public ErrorSenderWorkerSpecifications()
        {
            Glitch.Config.WithNotificationsMaxBatchSize(2);
        }

        [TestInitialize]
        public void TestInitialize()
        {
            _testErrorSenderWorker = new TestErrorSenderWorker();
            ErrorQueue.Clear();
        }

        [TestMethod]
        public void Given_the_queue_has_no_items_Should_not_notify()
        {
            _testErrorSenderWorker.Start();

            Assert.IsTrue(_testErrorSenderWorker.Stop(TimeSpan.FromSeconds(10)));

            Assert.IsFalse(_testErrorSenderWorker.DeliveredTask.IsCompleted);
        }

        [TestMethod]
        public void Given_the_queue_has_less_items_than_the_max_batch_Should_wait()
        {
            _testErrorSenderWorker.Start();

            ErrorQueue.Push(new Error("error"));

            Assert.IsFalse(_testErrorSenderWorker.DeliveredTask.Wait(TimeSpan.FromSeconds(1)));

            ErrorQueue.Push(new Error("error2"));

            Assert.IsTrue(_testErrorSenderWorker.DeliveredTask.Wait(TimeSpan.FromSeconds(1)));

            Assert.AreEqual(2, _testErrorSenderWorker.DeliveredTask.Result.Errors.Length);

            Assert.IsTrue(_testErrorSenderWorker.Stop(TimeSpan.FromSeconds(10)));
        }

        [TestMethod]
        public void Given_the_queue_has_less_items_than_the_max_batch_Should_not_wait()
        {
            ErrorQueue.Push(new Error("error"));

            ErrorQueue.Push(new Error("error2"));

            _testErrorSenderWorker.Start();

            Assert.IsTrue(_testErrorSenderWorker.DeliveredTask.Wait(TimeSpan.FromSeconds(1)));

            Assert.AreEqual(2, _testErrorSenderWorker.DeliveredTask.Result.Errors.Length);

            Assert.IsTrue(_testErrorSenderWorker.Stop(TimeSpan.FromSeconds(10)));
        }

        [TestMethod]
        public void Given_the_worker_is_stopped_Should_notify_pending_queue_notifications()
        {
            ErrorQueue.Push(new Error("error"));

            _testErrorSenderWorker.Start();

            Assert.IsFalse(_testErrorSenderWorker.DeliveredTask.Wait(TimeSpan.FromSeconds(1)));

            Assert.IsTrue(_testErrorSenderWorker.Stop(TimeSpan.FromSeconds(10)));

            Assert.IsTrue(_testErrorSenderWorker.DeliveredTask.Wait(TimeSpan.Zero));

            Assert.AreEqual(1, _testErrorSenderWorker.DeliveredTask.Result.Errors.Length);
        }
    }

    public class TestErrorSenderWorker : ErrorSenderWorker
    {
        private readonly TaskCompletionSource<ErrorBatch> _taskCompletionSource = new TaskCompletionSource<ErrorBatch>();

        public Task<ErrorBatch> DeliveredTask { get { return _taskCompletionSource.Task; } }
        protected override void Send(ErrorBatch errors)
        {
            _taskCompletionSource.SetResult(errors);
        }
    }
}
