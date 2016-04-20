using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Misfit.AddIn
{
    public class FrameworkEventArgs
    {
        private IBundle bundle;
        private FrameworkState state;
        private Exception exception;

        public IBundle Bundle
        {
            get { return bundle; }
        }

        public FrameworkState State
        {
            get { return state; }
        }

        public Exception Exception
        {
            get { return exception; }
        }

        public FrameworkEventArgs(IBundle bundle, FrameworkState state,
            Exception exception)
        {
            this.bundle = bundle;
            this.state = state;
            this.exception = exception;
        }
    }
}
