using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Misfit.AddIn
{
    public interface IPluginContext
    {
        IPlugin CurrentPlugin { get; }

        IPlugin GetPlugin(string symbolicName);

        void RegisterService(string[] clazzes,
            object service, Dictionary<string, object> properties);

        object GetService(string clazz);

        object GetService(Type type);

        T GetService<T>();
    }
}
