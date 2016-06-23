using MisfitThemes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Misfit.Desktop.Shell
{
    public class App : MisfitApplication
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            MisfitWeaver.Initailize();

            MisfitBootstrapper defaultBootstrapper = new MisfitBootstrapper(this);
            defaultBootstrapper.Run();
        }


        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            MisfitWeaver.Dispose();
        }
    }
}
