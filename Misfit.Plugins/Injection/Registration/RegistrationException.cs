using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Misfit.Plugins.Injection.Registration
{
    public class RegistrationException : InjectionException
    {
        public RegistrationException(string message)
            : base(message)
        {

        }
    }
}
