using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Misfit.Plugins.Injection.Activation
{
    public class ActivationException : InjectionException
    {
        public ActivationException(string message)
            : base(message)
        {

        }
    }
}
