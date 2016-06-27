using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Misfit.IO
{
    public class ModuleNode
    {
        public ModuleNode()
        {
            this.ConnectionStringNodes = new List<ArgumentNode>();
        }
        public string Name { set; get; }

        public string Location { set; get; }

        public string Description { set; get; }

        public List<ArgumentNode> ConnectionStringNodes { set; get; }
    }
}
