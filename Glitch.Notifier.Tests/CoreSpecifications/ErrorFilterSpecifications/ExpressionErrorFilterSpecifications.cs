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
    public class ExpressionErrorFilterSpecifications
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Given_expression_is_null_Should_throw_an_exception()
        {
            new ExpressionErrorFilter(null);
        }

        [TestMethod]
        public void Given_expression_does_not_match_Should_not_exclude_error()
        {
            //Arrange
            var error = new Error(new ArgumentException());

            //Act
            var exclude = new ExpressionErrorFilter(e => e.OccurredAt 
                                                        < DateTime.UtcNow.AddHours(-1))
                                .Exclude(error);

            //Assert
            Assert.IsFalse(exclude);
        }

        [TestMethod]
        public void Given_expression_does_match_Should_exclude_error()
        {
            //Arrange
            var error = new Error(new ArgumentException());

            //Act
            var exclude = new ExpressionErrorFilter(e => e.OccurredAt
                                                       > DateTime.UtcNow.AddHours(-1))
                               .Exclude(error);

            //Assert
            Assert.IsTrue(exclude);
        }
    }
}
