using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Misfit.Core
{
    public class MisfitDescriptor
    {
        public MisfitDescriptor()
        {
            this.PluginDescriptors = new List<ModuleDescriptor>();
        }

        public string MisfitConnectionString { set; get; }

        public List<ModuleDescriptor> PluginDescriptors { set; get; }
    }
}
