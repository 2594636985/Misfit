using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Misfit.AddIn
{
    [Serializable]
    public class BundleException : Exception
    {
        public BundleException()
        {
            
        }

        public BundleException(string message)
            : base(message)
        {

        }

        public BundleException(string message, Exception inner)
            : base(message, inner)
        {

        }

        protected BundleException(SerializationInfo info,
            StreamingContext context)
            : base(info, context)
        {
        }
    }
}
