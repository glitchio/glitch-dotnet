using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Glitch.Notifier.AspNet;
using Glitch.Notifier.ErrorContentFilters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Glitch.Notifier.Tests.AspNetSpecifications
{
    [TestClass]
    public class ErrorContentFilterSpecifications : AspNetSpecificationBase
    {
        [TestMethod]
        public void Should_filter_cookies()
        {
            var cookies = new HttpCookieCollection
                              {
                                  new HttpCookie("Test1", "value"), 
                                  new HttpCookie("ASPXAUTH", "value2")
                              };
            HttpRequest.Setup(r => r.Cookies).Returns(cookies);

            var wrapper =
                Glitch.Factory.HttpContextError(new ArgumentException(), HttpContext.Object)
                .WithCookies();

            Assert.AreEqual("value", 
                ((Dictionary<string, string>)wrapper.Error.ExtraData["Cookies"])["Test1"]);
            Assert.AreEqual(ErrorContentFilter.ProtectedText,
                ((Dictionary<string, string>)wrapper.Error.ExtraData["Cookies"])["ASPXAUTH"]);
        }

        [TestMethod]
        public void Should_filter_server_variables()
        {
            var serverVariables = new NameValueCollection
                                      {
                                          {"Test1", "value"},
                                          {"ASPXAUTH", "value2"}
                                      };
            HttpRequest.Setup(r => r.ServerVariables).Returns(serverVariables);

            var wrapper =
                Glitch.Factory.HttpContextError(new ArgumentException(), HttpContext.Object)
                .WithServerVariables();

            Assert.AreEqual("value",
                ((Dictionary<string, string>)wrapper.Error.ExtraData["ServerVariables"])["Test1"]);
            Assert.AreEqual(ErrorContentFilter.ProtectedText,
                ((Dictionary<string, string>)wrapper.Error.ExtraData["ServerVariables"])["ASPXAUTH"]);
        }

        [TestMethod]
        public void Should_filter_form()
        {
            var form = new NameValueCollection
                                      {
                                          {"Test1", "value"},
                                          {"_VIEWSTATE", "value2"}
                                      };
            HttpRequest.Setup(r => r.Form).Returns(form);

            var wrapper =
                Glitch.Factory.HttpContextError(new ArgumentException(), HttpContext.Object)
                .WithForm();

            Assert.AreEqual("value",
                ((Dictionary<string, string>)wrapper.Error.ExtraData["Form"])["Test1"]);
            Assert.AreEqual(ErrorContentFilter.ProtectedText,
                ((Dictionary<string, string>)wrapper.Error.ExtraData["Form"])["_VIEWSTATE"]);
        }
    }
}
