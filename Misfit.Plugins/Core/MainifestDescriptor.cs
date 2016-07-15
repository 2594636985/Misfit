using Misfit.Plugins.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Misfit.Plugins.Core
{
    public class Mainifest
    {
        public Mainifest()
        {
            this.ServiceDescriptors = new List<ServiceDescriptor>();
        }

        public string Name { set; get; }

        public string ConnectString { set; get; }

        public List<ServiceDescriptor> ServiceDescriptors { set; get; }
    }
}
