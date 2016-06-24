using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Misfit.Modulation
{
    public class MisfitDescriptor
    {
        public MisfitDescriptor()
        {
            this.PluginDescriptors = new List<Module>();
        }

        public string MisfitConnectionString { set; get; }

        public List<Module> PluginDescriptors { set; get; }
    }
}
