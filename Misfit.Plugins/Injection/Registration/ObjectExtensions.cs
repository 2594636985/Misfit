using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Misfit.Plugins.Injection.Registration
{
    internal static class ObjectExtensions
    {
        internal static IDictionary<string, object> ToNamedArgumentDictionary(this object source)
        {
            return source
                .GetType()
                .GetProperties()
                .ToDictionary(property => property.Name, property => property.GetValue(source, null));
        }
    }
}
