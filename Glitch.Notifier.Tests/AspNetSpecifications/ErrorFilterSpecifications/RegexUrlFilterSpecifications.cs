using System;
using System.Collections.Generic;
using System.Linq;
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
    public class RegexUrlFilterSpecifications : AspNetSpecificationBase
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Given_contains_url_fragment_is_null_Should_throw_an_exception()
        {
            new RegexUrlErrorFilter(null);
        }

        [TestMethod]
        public void Given_url_is_not_available__Should_not_exclude_error()
        {
            //Arrange
            var error = new Error("test");

            //Act
            var exclude = new RegexUrlErrorFilter("a").Exclude(error);

            //Assert
            Assert.IsFalse(exclude);
        }

        [TestMethod]
        public void Given_url_fragment_does_not_match_Should_not_exclude_error()
        {
            //Arrange
            HttpRequest.Setup(r => r.Url).Returns(new Uri("http://test/a/b/3"));
            var wrapper =
                Glitch.Factory.HttpContextError(new ArgumentException(), HttpContext.Object)
                .WithUrl();

            //Act
            var exclude = new RegexUrlErrorFilter("c").Exclude(wrapper.Error);

            //Assert
            Assert.IsFalse(exclude);
        }

        [TestMethod]
        public void Given_url_fragment_does_match_Should_exclude_error()
        {
            HttpRequest.Setup(r => r.Url).Returns(new Uri("http://test/a/b?Id=5"));
            var wrapper =
                Glitch.Factory.HttpContextError(new ArgumentException(), HttpContext.Object)
                .WithUrl();

            //Act
            var exclude = new RegexUrlErrorFilter("a/b\\?Id=\\d+").Exclude(wrapper.Error);

            //Assert
            Assert.IsTrue(exclude);
        }
    }
}
