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
                //For the hash, only the error message and the first line of the stacktrace 
                //are considered.
                hashSeed = string.Format("{0}|{1}", error.Exception.Message, 
                                         error.Exception.GetStackTraceFirstLine());
            }
            else
            {
                hashSeed = error.ErrorMessage;
            }

            return Crypto.Hash(hashSeed);
        }
    }
}
