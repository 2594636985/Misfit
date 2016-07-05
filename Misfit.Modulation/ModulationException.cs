using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Misfit.Modulation
{
    public class ModulationException : Exception
    {
        public ModulationException(string message)
            : base(message)
        {

        }

        public ModulationException(string message, Exception inner)
            : base(message, inner)
        {


        }
    }
}
