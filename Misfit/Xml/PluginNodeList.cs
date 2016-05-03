using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Misfit.Xml
{
    public class PluginNodeList : List<PluginNode>
    {
        public bool Debug { set; get; }

        public string DebugName { set; get; }
    }
}
