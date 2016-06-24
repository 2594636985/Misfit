using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Misfit.Modulation.IO
{
    public class ModuleNode
    {
        public ModuleNode()
        {
            this.ConnectionStringNodes = new List<ConnectionStringNode>();
        }
        public string Name { set; get; }

        public string Location { set; get; }

        public string Description { set; get; }

        public List<ConnectionStringNode> ConnectionStringNodes { set; get; }
    }
}
