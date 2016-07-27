using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Misfit.Plugins
{
    public class PluginException : Exception
    {
        public PluginException(string message)
            : base(message)
        {

        }
    }
}
