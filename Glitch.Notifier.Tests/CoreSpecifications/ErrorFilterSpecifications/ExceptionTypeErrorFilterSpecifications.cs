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
    public class ExceptionTypeErrorFilterSpecifications
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Given_exception_type_is_null_Should_throw_an_exception()
        {
            new ExceptionTypesErrorFilter(null);
        }

        [TestMethod]
        public void Given_error_was_not_created_from_an_exception_Should_not_excluce_error()
        {
            //Arrange
            var error = new Error("Error");

            //Act
            var exclude = new ExceptionTypesErrorFilter(typeof(InvalidOperationException)).Exclude(error);

            //Assert
            Assert.IsFalse(exclude);
        }

        [TestMethod]
        public void Given_exception_type_does_not_match_Should_not_excluce_error()
        {
            //Arrange
            var error = new Error(new ArgumentException());

            //Act
            var exclude = new ExceptionTypesErrorFilter(typeof(InvalidOperationException)).Exclude(error);

            //Assert
            Assert.IsFalse(exclude);
        }

        [TestMethod]
        public void Given_exceptiont_type_does_match_Should_exclude_error()
        {
            //Arrange
            var error = new Error(new ArgumentException());

            //Act
            var exclude = new ExceptionTypesErrorFilter(typeof(ArgumentException)).Exclude(error);

            //Assert
            Assert.IsTrue(exclude);
        }
    }
}
