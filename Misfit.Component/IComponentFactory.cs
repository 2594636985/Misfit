using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Misfit.Component
{
    public interface IComponentFactory
    {
        IComponentInstance NewInstance(Dictionary<string, object> properties);
    }
}
