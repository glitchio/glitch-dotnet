using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Glitch.Notifier.Tests.CoreSpecifications
{
    [TestClass]
    public class GlitchConfigSpecifications
    {
        [TestMethod]
        public void Given_https_is_enabled_Should_reflect_it_in_the_url()
        {
            Glitch.Config.UseHttps(true);

            Glitch.Config.Url.StartsWith("https");
        }

        [TestMethod]
        public void Given_https_is_not_enabled_Should_reflect_it_in_the_url()
        {
            Glitch.Config.UseHttps(false);

            Glitch.Config.Url.StartsWith("http");
        }
    }
}
