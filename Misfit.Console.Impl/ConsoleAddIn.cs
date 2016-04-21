using System;
using Misfit.AddIn;

namespace Misfit.Console
{
    [Serializable]
    [AddIn("ConsoleAddIn")]
    public class ConsoleAddIn : AddInBase
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
            //shell = context.GetService<IShell>();

            console = new ConsoleService();
            console.Start();
        }

        public override void Stop(IPluginContext context)
        {
            if (console != null)
            {
                console.Stop();
            }
        }
    }
}
