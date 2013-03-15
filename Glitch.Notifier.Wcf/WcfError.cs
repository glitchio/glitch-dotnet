using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace Glitch.Notifier.Wcf
{
    public class WcfError : ErrorContextWrapper
    {
        private readonly OperationContext _context;

        public WcfError(Exception exception, OperationContext context)
            : base(new Error(exception))
        {
            _context = context;
            Error.WithLocation(context.Channel.LocalAddress.Uri.ToString())
                .SetPlatform("WCF");
        }

        public WcfError WithContextData()
        {
            return
                WithServiceName()
                .WithContractName()
                .WithBindingName();
        }

        public WcfError WithServiceName()
        {
            Error.With("ServiceName", _context.Host.Description.ConfigurationName);
            return this;
        }

        public WcfError WithContractName()
        {
            Error.With("ContractName", _context.EndpointDispatcher.ContractName);
            return this;
        }

        public WcfError WithBindingName()
        {
            Error.With("BindingName", _context.EndpointDispatcher.ChannelDispatcher.BindingName);
            return this;
        }


        public WcfError WithErrorProfile(string errorProfile)
        {
            Error.WithErrorProfile(errorProfile);
            return this;
        }
    }
}
