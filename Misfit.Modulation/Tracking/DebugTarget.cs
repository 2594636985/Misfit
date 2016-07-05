using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Misfit.Modulation.Tracking
{
    public class DebugTarget : LayoutTarget
    {
        public override void WriteLine(string message)
        {
            Debug.WriteLine(message);
        }

        public override void WriteLine(string message, TrackerLevel level)
        {
            Debug.WriteLine(message);
        }
    }
}
