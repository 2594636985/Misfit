using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Misfit.Plugins.Configuration
{
    public class MainifestNode
    {
        public MainifestNode()
        {
            this.ServiceNodes = new List<ServiceNode>();
        }
        public string Name { set; get; }

        public string ConnectString { set; get; }

        public List<ServiceNode> ServiceNodes { set; get; }
    }
}
