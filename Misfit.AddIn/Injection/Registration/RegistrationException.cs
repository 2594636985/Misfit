using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Misfit.AddIn.Injection.Registration
{
    public class RegistrationException : InjectionException
    {
        public RegistrationException(string message)
            : base(message)
        {

        }
    }
}
