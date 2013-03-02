using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Glitch.Notifier.ErrorFilters;
using Glitch.Notifier.ErrorContentFilters;

namespace Glitch.Notifier
{
    class SampleUsage
    {
        public void Test()
        {
            Glitch.Config.UseApiKey("234234324")
                .UseHttps(true)
                .UseDefaultErrorProfile("my-profile");

            Glitch.Config.IgnoreErrors.WithErrorMessageContaining("test");

            Glitch.Factory.Error("error")
                  .WithErrorProfile("my-other-profile")
                  .With("controller", "AccountController")
                  .Send();
        }
    }
}
