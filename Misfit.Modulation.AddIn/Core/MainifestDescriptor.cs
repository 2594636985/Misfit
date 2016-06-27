using Misfit.Modulation.AddIn.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Misfit.Modulation.AddIn.Core
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
