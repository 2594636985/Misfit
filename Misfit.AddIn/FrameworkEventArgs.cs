using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Misfit.AddIn
{
    public class FrameworkEventArgs
    {
        private IPlugin _plugin;
        private FrameworkState state;
        private Exception exception;

        public IPlugin Bundle
        {
            get { return _plugin; }
        }

        public FrameworkState State
        {
            get { return state; }
        }

        public Exception Exception
        {
            get { return exception; }
        }

        public FrameworkEventArgs(IPlugin plugin, FrameworkState state,
            Exception exception)
        {
            this._plugin = plugin;
            this.state = state;
            this.exception = exception;
        }
    }
}
