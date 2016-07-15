using Misfit.Plugins.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Misfit.Plugins
{
    /// <summary>
    /// 模块域的上下文
    /// </summary>
    public interface IPluginDomainContext
    {
        Dictionary<string, string> Variables { set; get; }

        Dictionary<string, string> Parameters { set; get; }

        object GetService(string assemblyCatalogName, string typeName);

        TInterface GetService<TInterface>(string assemblyCatalogName);

    }
}
