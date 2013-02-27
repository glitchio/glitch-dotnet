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
    public class RegexErrorMessageFilterSpecifications
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Given_expression_is_null_Should_throw_an_exception()
        {
            new RegexErrorMessageFilter(null);
        }

        [TestMethod]
        public void Given_regex_does_not_match_Should_not_exclude_error()
        {
            //Arrange
            var error = new Error("test");

            //Act
            var exclude = new RegexErrorMessageFilter("d+").Exclude(error);

            //Assert
            Assert.IsFalse(exclude);
        }

        [TestMethod]
        public void Given_regex_does_match_Should_exclude_error()
        {
            //Arrange
            var error = new Error("test");

            //Act
            var exclude = new RegexErrorMessageFilter(".+").Exclude(error);

            //Assert
            Assert.IsTrue(exclude);
        }
    }
}
