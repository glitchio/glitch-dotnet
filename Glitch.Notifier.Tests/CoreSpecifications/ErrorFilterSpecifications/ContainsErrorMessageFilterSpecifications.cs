using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Glitch.Notifier.ErrorFilters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Glitch.Notifier.Tests.CoreSpecifications.ErrorFilterSpecifications
{
    [TestClass]
    public class ContainsErrorMessageFilterSpecifications
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Given_contains_message_is_null_Should_throw_an_exception()
        {
            new ContainsErrorMessageFilter(null);
        }

        [TestMethod]
        public void Given_regex_does_not_match_Should_not_exclude_error()
        {
            //Arrange
            var error = new Error("test");

            //Act
            var exclude = new ContainsErrorMessageFilter("a").Exclude(error);

            //Assert
            Assert.IsFalse(exclude);
        }

        [TestMethod]
        public void Given_regex_does_match_Should_exclude_error()
        {
            //Arrange
            var error = new Error("test");

            //Act
            var exclude = new ContainsErrorMessageFilter("e").Exclude(error);

            //Assert
            Assert.IsTrue(exclude);
        }
    }
}
