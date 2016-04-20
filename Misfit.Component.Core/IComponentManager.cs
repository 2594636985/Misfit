using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Misfit.Component.Metadata;

namespace Misfit.Component.Core
{
    internal interface IComponentManager : IComponent, IDisposable
    {
        void Update();

        ComponentInfo Metadata { get; }
    }
}
