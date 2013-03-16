using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glitch.Notifier
{
    static class GroupKeyDefaultGenerator
    {
        public static string Compute(Error error)
        {
            string hashSeed;
            //If an exception was provided, we have more info to create a groupKey
            if (error.Exception != null)
            {
                var stackTraceFirstLine = error.Exception.GetStackTraceFirstLine();
                //For the hash, we take the error type and the first line of the stack trace
                //If the stack trace is no available (an Exception that wasn't thrown is passed)
                //then we take the error message instead.
                hashSeed = string.Format("{0}|{1}", error.Exception.GetType().Name,
                                        stackTraceFirstLine ?? error.ErrorMessage);
            }
            else
            {
                hashSeed = error.ErrorMessage;
            }

            return Crypto.Hash(hashSeed);
        }
    }
}
