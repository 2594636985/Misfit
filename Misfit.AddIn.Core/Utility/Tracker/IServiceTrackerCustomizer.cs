using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Misfit.AddIn.Core;

namespace Misfit.AddIn.Utility.Tracker
{
    public interface IServiceTrackerCustomizer
    {
        object AddingService(ServiceReference reference);

        void ModifiedService(ServiceReference reference,
            Object service);

        void RemovedService(ServiceReference reference,
            Object service);
    }
}
