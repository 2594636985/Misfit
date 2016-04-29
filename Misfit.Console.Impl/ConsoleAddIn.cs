using System;
using Misfit.AddIn;
using System.Threading;

namespace Misfit.Console
{
    public class ConsoleAddIn : Activator
    {
        static IPluginContext context = null;

        private ConsoleService console = null;

        public static IPluginContext Context
        {
            get
            {
                return context;
            }
        }
        public override void Start(IPluginContext context)
        {
            ConsoleAddIn.context = context;
            this.LaunchDesktop();
        }

        public override void Stop(IPluginContext context)
        {
            if (console != null)
            {
                console.Stop();
            }
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

            //shell = context.GetService<IShell>();

            console = new ConsoleService();
            console.Start();
        }

    }
}
