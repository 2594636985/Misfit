using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Misfit.Plugins.Tracking
{
    public interface ITracker
    {
        void Info(string message);

        void Error(string message);
    }
}
