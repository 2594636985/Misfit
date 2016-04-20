﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Misfit.Component
{
    [Serializable]
    public class ComponentException : Exception
    {
        public ComponentException() { }
        public ComponentException(string message) : base(message) { }
        public ComponentException(string message, Exception inner) : base(message, inner) { }
        protected ComponentException(
          SerializationInfo info,
          StreamingContext context)
            : base(info, context) { }
    }
}
