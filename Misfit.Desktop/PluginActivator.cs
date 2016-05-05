using Misfit.AddIn;
using Misfit.AddIn.Cmd;
using Misfit.AddIn.Pipe;
using Misfit.Desktop.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Misfit.Desktop
{
    [Serializable]
    public class PluginActivator : AddInActivator
    {
        private static IPluginContext _currentContext = null;
        public static IPluginContext CurrentContext
        {
            get
            {
                return _currentContext;
            }
        }

        public override void Start(string[] args)
        {
            Commands.Execute();
            App app = new App();
            app.InitializeComponent();
            app.Run();
        }

        public override void Stop(string[] args)
        {

        }
    }
}
