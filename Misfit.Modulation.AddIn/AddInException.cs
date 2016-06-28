using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Misfit.Modulation.AddIn
{
    public class AddInException : Exception
    {
        public AddInException(string message)
            : base(message)
        {

        }
    }
}
