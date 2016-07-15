using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Misfit.Plugins.Core
{
    public class CoreException : PluginException
    {
        public CoreException(string message)
            : base(message)
        {

        }
    }
}
