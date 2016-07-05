using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Misfit.Modulation.Tracking
{
    public interface ITarget
    {
        void WriteLine(string message);

        void WriteLine(string message, TrackerLevel level);
    }
}
