using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Misfit.Modulation.Tracking
{
    public class NullTarget : ITarget
    {
        public void WriteLine(string message)
        {
        }


        public void WriteLine(string message, TrackerLevel level)
        {
        }
    }
}
