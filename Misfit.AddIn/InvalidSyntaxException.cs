using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Misfit.AddIn
{
    [Serializable]
    public class InvalidSyntaxException : Exception
    {
        private string filter;

        public string Filter
        {
            get { return filter; }
        }

        public InvalidSyntaxException(string message, string filter)
            : base(message)
        {
            this.filter = filter;
        }

        public InvalidSyntaxException(string message, string filter,
            Exception inner)
            : base(message, inner)
        {
            this.filter = filter;
        }
    }
}
