using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Misfit.Component
{
    public interface IComponentInstance : IDisposable
    {
        object Instance { get; }
    }
}
