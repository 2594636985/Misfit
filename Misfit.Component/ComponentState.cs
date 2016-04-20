using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Misfit.Component
{
    public enum ComponentState
    {
        Disabled = 1,
        Enabled = 2,
        Unsatisfied = 4,
        Activating = 8,
        Active = 16,
        Registered = 32,
        Factory = 64,
        Deactivating = 128,
        Destroyed = 256
    }
}