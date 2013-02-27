using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Moq;

namespace Glitch.Notifier.Tests.AspNetSpecifications.ErrorFilterSpecifications
{
    public class ErrorFilterSpecificationBase
    {
        protected readonly Mock<HttpContextBase> HttpContext = new Mock<HttpContextBase>();
        protected readonly Mock<HttpRequestBase> HttpRequest = new Mock<HttpRequestBase>();
        protected readonly Mock<HttpResponseBase> HttpResponse = new Mock<HttpResponseBase>();

        public ErrorFilterSpecificationBase()
        {
            HttpContext.Setup(x => x.Request).Returns(HttpRequest.Object);
            HttpContext.Setup(x => x.Response).Returns(HttpResponse.Object);
        }
    }
}
