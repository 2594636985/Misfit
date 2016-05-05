﻿using Misfit.AddIn;
using Misfit.AddIn.Cmd;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Misfit.Cmd
{
    public abstract class AbstractCommand : ICommand
    {
        private IPluginFramework _pluginFramework;
        public AbstractCommand(IPluginFramework pluginFramework)
        {
            this._pluginFramework = pluginFramework;
        }

        public abstract string CommandName { get; }

        public abstract object Execute();
    }
}
