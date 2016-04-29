using Misfit.AddIn;
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

        public override void Start(IPluginContext context)
        {
            _currentContext = context;

            LaunchDesktop();

        }

        public override void Stop(IPluginContext context)
        {

        }
        private void LaunchDesktop()
        {
            ThreadStart start = new ThreadStart(LaunchDesktopRun);
            Thread uiThread = new Thread(start);
            uiThread.SetApartmentState(ApartmentState.STA);
            uiThread.IsBackground = false;
            uiThread.Start();
        }

        private void LaunchDesktopRun()
        {
            App app = new App();
            app.InitializeComponent();
            app.Run();
        }
    }
}
