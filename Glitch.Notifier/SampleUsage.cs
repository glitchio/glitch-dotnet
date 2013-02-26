using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glitch.Notifier
{
    class SampleUsage
    {
        public void Test()
        {
            Glitch.Config.UseApiKey("234234324")
                .UseHttps(true)
                .UseDefaultErrorProfile("my-profile");

            Glitch.Factory.Error("error")
                  .WithErrorProfile("my-other-profile")
                  .With("controller", "AccountController")
                  .Send();
        }
    }
}
