using Misfit.AddIn.Serices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Misfit.AddIn
{
    public interface IModuleDomainContext
    {
        Dictionary<string, string> ConnectionStrings { set; get; }

        object GetService(string assemblyCatalogName, string typeName);

        TInterface GetService<TInterface>(string assemblyCatalogName) where TInterface : IMisfitService;

    }
}
