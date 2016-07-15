using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Misfit.Domain
{
    public class PluginDomainException : Exception
    {
        public PluginDomainException(string message)
            : base(message)
        {

        }

        public PluginDomainException(string message, Exception inner)
            : base(message, inner)
        {

        }
    }
}
