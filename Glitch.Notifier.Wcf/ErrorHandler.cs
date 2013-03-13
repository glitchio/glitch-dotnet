using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;

namespace Glitch.Notifier.Wcf
{
    public class ErrorHandler : IErrorHandler
    {
        public void ProvideFault(Exception error, MessageVersion version, ref Message fault)
        {

        }

        /// <summary>
        /// Enables error-related processing and returns a value that indicates whether subsequent HandleError implementations are called.
        /// </summary>
        /// <param name="error">The exception thrown during processing.</param>
        /// <returns>
        /// true if subsequent <see cref="T:System.ServiceModel.Dispatcher.IErrorHandler"/> implementations must not be called; otherwise, false. The default is false.
        /// </returns>
        public bool HandleError(Exception error)
        {
            Glitch.Factory.Error(error)
                            .Send();
            return false;
        }
    }
}
