using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Glitch.Notifier.AspNet;
using Glitch.Notifier.AspNet.ErrorFilters;
using Glitch.Notifier.ErrorFilters;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Glitch.Notifier.Tests.AspNetSpecifications.ErrorFilterSpecifications
{
    [TestClass]
    public class HttpCodeErrorFilterSpecifications
    {
        private readonly Mock<HttpContextBase> _httpContext = new Mock<HttpContextBase>();
        private readonly Mock<HttpRequestBase> _httpRequest = new Mock<HttpRequestBase>();
        private readonly Mock<HttpResponseBase> _httpResponse = new Mock<HttpResponseBase>();

        public HttpCodeErrorFilterSpecifications()
        {
            _httpContext.Setup(x => x.Request).Returns(_httpRequest.Object);
            _httpContext.Setup(x => x.Response).Returns(_httpResponse.Object);
        }

        [TestMethod]
        public void Given_http_code_is_not_available__Should_not_exclude_error()
        {
            //Arrange
            var error = new Error("test");

            //Act
            var exclude = new HttpCodeErrorFilter(HttpStatusCode.NotFound).Exclude(error);

            //Assert
            Assert.IsFalse(exclude);
        }


        [TestMethod]
        public void Given_http_code_does_not_match_Should_not_exclude_error()
        {
            //Arrange
            _httpResponse.Setup(r => r.StatusCode).Returns(401);
            var wrapper =
                Glitch.Factory.HttpContextError(new ArgumentException(), _httpContext.Object).WithHttpStatusCode();

            //Act
            var exclude = new HttpCodeErrorFilter(HttpStatusCode.NotFound).Exclude(wrapper.Error);

            //Assert
            Assert.IsFalse(exclude);
        }

        [TestMethod]
        public void Given_http_code_does_match_Should_exclude_error()
        {
            //Arrange
            _httpResponse.Setup(r => r.StatusCode).Returns(404);
            var wrapper = Glitch.Factory.HttpContextError(new ArgumentException(), _httpContext.Object).WithHttpStatusCode();

            //Act
            var exclude = new HttpCodeErrorFilter(HttpStatusCode.NotFound).Exclude(wrapper.Error);

            //Assert
            Assert.IsTrue(exclude);
        }
    }
}
