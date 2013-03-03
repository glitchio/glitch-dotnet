using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Glitch.Notifier.ErrorContentFilters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Glitch.Notifier.Tests.CoreSpecifications
{
    [TestClass]
    public class ErrorContentFilterSpecifications
    {
        public ErrorContentFilterSpecifications()
        {
            Glitch.Config.IgnoreContent
                .FromDataGroupWithFieldsContaining("DataGroupKey","TestField");
        }

        [TestMethod]
        public void Given_field_is_not_to_be_filtered_Should_do_nothing()
        {
            var input = new Dictionary<string, string> {{"Test1Field", "value"}};

            Glitch.Config.IgnoreContent.Filter("DataGroupKey", input);

            Assert.AreEqual("value", input["Test1Field"]);
        }

        [TestMethod]
        public void Given_field_to_be_filtered_is_present_Should_filter_it()
        {
            var input = new Dictionary<string, string> { { "1TestField4", "value" } };

            Glitch.Config.IgnoreContent.Filter("DataGroupKey", input);

            Assert.AreEqual(ErrorContentFilter.ProtectedText, input["1TestField4"]);
        }
    }
}
