using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web;
using Glitch.Notifier.AspNet;
using Glitch.Notifier.ErrorContentFilters;
using Glitch.Notifier.AspNet.ErrorContentFilters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Glitch.Notifier.Tests.AspNetSpecifications
{
    [TestClass]
    public class ErrorContentFilterSpecifications : AspNetSpecificationBase
    {
        [TestMethod]
        public void Should_filter_cookies()
        {
            Glitch.Config.IgnoreContent
                .FromCookiesWithNamesContaining(".ASPXAUTH");
            var cookies = new HttpCookieCollection
                              {
                                  new HttpCookie("Test1", "value"), 
                                  new HttpCookie(".ASPXAUTH", "value2")
                              };
            HttpRequest.Setup(r => r.Cookies).Returns(cookies);

            var wrapper =
                Glitch.Factory.HttpContextError(new ArgumentException(), HttpContext.Object)
                .WithCookies();

            Assert.AreEqual("value", 
                ((Dictionary<string, string>)wrapper.Error.ErrorData["Cookies"])["Test1"]);
            Assert.AreEqual(ErrorContentFilter.ProtectedText,
                ((Dictionary<string, string>)wrapper.Error.ErrorData["Cookies"])[".ASPXAUTH"]);
        }

        [TestMethod]
        public void Should_filter_server_variables()
        {
            Glitch.Config.IgnoreContent
                .FromServerVariablesWithNamesContaining("AUTH_PASSWORD");
            var serverVariables = new NameValueCollection
                                      {
                                          {"Test1", "value"},
                                          {"AUTH_PASSWORD", "value2"}
                                      };
            HttpRequest.Setup(r => r.ServerVariables).Returns(serverVariables);

            var wrapper =
                Glitch.Factory.HttpContextError(new ArgumentException(), HttpContext.Object)
                .WithServerVariables();

            Assert.AreEqual("value",
                ((Dictionary<string, string>)wrapper.Error.ErrorData["ServerVariables"])["Test1"]);
            Assert.AreEqual(ErrorContentFilter.ProtectedText,
                ((Dictionary<string, string>)wrapper.Error.ErrorData["ServerVariables"])["AUTH_PASSWORD"]);
        }

        [TestMethod]
        public void Should_filter_form()
        {
            Glitch.Config.IgnoreContent
                .FromFormVariablesWithNamesContaining("_VIEWSTATE");
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
                ((Dictionary<string, string>)wrapper.Error.ErrorData["Form"])["Test1"]);
            Assert.AreEqual(ErrorContentFilter.ProtectedText,
                ((Dictionary<string, string>)wrapper.Error.ErrorData["Form"])["_VIEWSTATE"]);
        }

        [TestMethod]
        public void Should_filter_http_headers()
        {
            Glitch.Config.IgnoreContent
                .FromHttpHeadersWithNamesContaining("Auhorization");
            var headers = new NameValueCollection
                                      {
                                          {"Test1", "value"},
                                          {"Auhorization", "value2"}
                                      };
            HttpRequest.Setup(r => r.Headers).Returns(headers);

            var wrapper =
                Glitch.Factory.HttpContextError(new ArgumentException(), HttpContext.Object)
                .WithHttpHeaders();

            Assert.AreEqual("value",
                ((Dictionary<string, string>)wrapper.Error.ErrorData["HttpHeaders"])["Test1"]);
            Assert.AreEqual(ErrorContentFilter.ProtectedText,
                ((Dictionary<string, string>)wrapper.Error.ErrorData["HttpHeaders"])["Auhorization"]);
        }
    }
}
