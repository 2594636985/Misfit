using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Misfit.AddIn
{
    [Serializable]
    public class PluginException : Exception
    {
        public PluginException()
        {
            
        }

        public PluginException(string message)
            : base(message)
        {

        }

        public PluginException(string message, Exception inner)
            : base(message, inner)
        {

        }

        protected PluginException(SerializationInfo info,
            StreamingContext context)
            : base(info, context)
        {
        }
    }
}
