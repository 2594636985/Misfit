using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Misfit.AddIn;
using Misfit.Component.Metadata;

namespace Misfit.Component
{
    public interface IComponent
    {
        long Id { get; }

        string Name { get; }

        ComponentState State { get; }

        IBundle Bundle { get; }

        string Factory { get; }

        bool IsServiceFactory { get; }

        string ClassName { get; }

        bool IsDefaultEnabled { get; }

        bool IsImmediate { get; }

        string[] GetServices();

        IDictionary<string, object> GetProperties();

        ReferenceInfo[] getReferences();

        void Enable();

        void Disable();
    }
}
