using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Misfit.AddIn.Core
{
    public class CoreException : Exception
    {
        public CoreException(string message)
            : base(message)
        {

        }
    }
}
