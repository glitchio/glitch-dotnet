﻿using System;
using System.Configuration;
using Glitch.Notifier.Notifications;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Glitch.Notifier.Tests.CoreSpecifications.ErrorSpecifications
{
    [TestClass]
    public class When_sending_an_error_notification
    {
        private readonly Mock<INotificationSender> _notificationSenderMock = new Mock<INotificationSender>();

        public When_sending_an_error_notification()
        {
            NotificationSenderFactory.SetFunc((url, apiKey) => _notificationSenderMock.Object);
        }

        [TestMethod]
        public void Given_notify_config_flag_is_set_to_false_Should_not_notify()
        {

            //arrange
            Glitch.Config.SendNotifications(false).UseApiKey("test");

            //act
            Glitch.Factory.Error("error")
                           .Send();

            //assert
            _notificationSenderMock.Verify(n => n.Send(It.IsAny<Error>()), Times.Never());
        }

        [TestMethod]
        public void Given_notify_config_flag_set_to_true_Should_notify()
        {

            //arrange
            Glitch.Config.SendNotifications(true).UseApiKey("test");

            //act
            var error = Glitch.Factory.Error("error");
            error.Send();

            //assert
            _notificationSenderMock.Verify(n => n.Send(error), Times.Once());
        }

        [TestMethod]
        [ExpectedException(typeof(ConfigurationErrorsException))]
        public void Given_apiKey_is_not_configured_Should_throw_an_exception()
        {
            //arrange
            Glitch.Config.UseApiKey(null).SendNotifications(true);

            //act
            Glitch.Factory.Error("error").Send();
        }

        [TestMethod]
        public void Given_group_key_is_specified_Should_not_change_it()
        {
            //arrange
            Glitch.Config.UseApiKey("test").SendNotifications(true);

            //act
            var error = Glitch.Factory.Error("test").WithGroupKey("groupKey");
            error.Send();

            //assert
            Assert.AreEqual("groupKey", error.GroupKey);
        }

        [TestMethod]
        public void Given_group_key_is_not_specified_Should_generate_one()
        {
            //arrange
            Glitch.Config.UseApiKey("test").SendNotifications(true);

            //act
            var error = Glitch.Factory.Error("test");
            Assert.IsNull(error.GroupKey);
            error.Send();

            //assert
            Assert.IsFalse(string.IsNullOrWhiteSpace(error.GroupKey));
        }

    }
}
