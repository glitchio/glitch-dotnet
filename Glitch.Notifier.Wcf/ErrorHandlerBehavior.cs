using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Configuration;

namespace Glitch.Notifier.Wcf
{
    public class ErrorHandlerBehavior : BehaviorExtensionElement
    {
        protected override object CreateBehavior()
        {
            return new ErrorServiceBehavior();
        }

        public override Type BehaviorType
        {
            get { return typeof(ErrorServiceBehavior); }
        }
    }
}
