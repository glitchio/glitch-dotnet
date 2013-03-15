using System;
using System.ServiceModel;

namespace Glitch.Notifier.Wcf
{
    public static class GlitchErrorFactoryExtensions
    {
        public static WcfError WcfError(this GlitchErrorFactory factory, Exception exception, OperationContext context, string errorProfile)
        {
            return new WcfError(exception, context)
                        .WithErrorProfile(errorProfile);
        }
    }
}