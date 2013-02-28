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
    public class ErrorFilterSpecifications
    {
        [TestMethod]
        public void Given_there_are_no_filters_Should_not_exclude_it()
        {
            var error = new Error("test");

            var exclude = new ErrorFilter().Exclude(error);

            Assert.IsFalse(exclude);
        }

        [TestMethod]
        public void Given_at_least_one_filter_matches_Should_exclude_it()
        {
            var error = new Error(new ArgumentException("test"));
            var pipeline = new ErrorFilter();
            pipeline.WithExceptionTypes(typeof(InvalidOperationException));
            pipeline.WithErrorMessageContaining("test");

            var exclude = pipeline.Exclude(error);

            Assert.IsTrue(exclude);
        }

        [TestMethod]
        public void Given_there_are_no_filters_that_match_Should_not_exclude_it()
        {
            var error = new Error(new ArgumentException());
            var pipeline = new ErrorFilter();
            pipeline.WithExceptionTypes(typeof (InvalidOperationException));
            pipeline.WithErrorMessageContaining("test");

            var exclude = pipeline.Exclude(error);

            Assert.IsFalse(exclude);
        }
    }
}
