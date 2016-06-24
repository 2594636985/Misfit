using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Misfit.Modulation.IO
{
    public class MisfitNode
    {
        public MisfitNode()
        {
            this.PluginNodes = new List<ModuleNode>();
        }
        public string MisfitConnectString { set; get; }

        public List<ModuleNode> PluginNodes { set; get; }
    }
}
