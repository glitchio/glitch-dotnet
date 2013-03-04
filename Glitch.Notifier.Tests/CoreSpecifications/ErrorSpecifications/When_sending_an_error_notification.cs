using System;
using System.Configuration;
using System.Security.Authentication;
using Glitch.Notifier.ErrorFilters;
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
            NotificationSenderFactory.SetFactoryFunc(() => _notificationSenderMock.Object);
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

        [TestMethod]
        public void Given_group_key_generator_is_specified_Should_use_it_to_compute_the_group_key()
        {
            //arrange
            Glitch.Config.UseApiKey("test").SendNotifications(true)
                    .WithGroupKeyGenerator(e => e.ErrorMessage);

            //act
            var error = Glitch.Factory.Error("test");
            error.Send();

            //assert
            Assert.AreEqual("test", error.GroupKey);
        }

        [TestMethod]
        public void Given_error_is_to_be_filtered_Should_not_notify()
        {
            //arrange
            Glitch.Config.SendNotifications(true).UseApiKey("test")
                .IgnoreErrors.WithExceptionTypes(typeof(InvalidCredentialException));

            //act
            Glitch.Factory.Error(new InvalidCredentialException()).Send();

            //assert
            _notificationSenderMock.Verify(n => n.Send(It.IsAny<Error>()), Times.Never());
        }
    }
}
