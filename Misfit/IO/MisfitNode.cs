using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Misfit.IO
{
    public class MisfitNode
    {
        public MisfitNode()
        {
            this.ArgumentNodes = new List<ArgumentNode>();
            this.PluginNodes = new List<ModuleNode>();
        }

        public List<ArgumentNode> ArgumentNodes { set; get; }

        public List<ModuleNode> PluginNodes { set; get; }
    }
}
