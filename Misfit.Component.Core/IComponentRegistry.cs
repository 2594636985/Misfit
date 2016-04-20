using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Misfit.Component.Core
{
    internal interface IComponentRegistry
    {
        long GenerateComponentId();

        void CheckComponentName(string name);

        void RegisterComponent(string name, IComponentManager component);

        void UnregisterComponent(string name);

        IComponentManager GetComponent(string name);
    }
}
