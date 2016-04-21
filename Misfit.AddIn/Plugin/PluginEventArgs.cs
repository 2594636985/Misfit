using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Misfit.AddIn
{
    public class PluginEventArgs : EventArgs
    {
        private IPlugin _plugin;
        private PluginTransition transition;

        public IPlugin Plugin
        {
            get { return _plugin; }
        }

        public PluginTransition Transition
        {
            get { return transition; }
        }

        public PluginEventArgs(PluginTransition transition, IPlugin plugin)
        {
            this._plugin = plugin;
            this.transition = transition;
        }
    }
}
