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




            MisfitBootstrapper defaultBootstrapper = new MisfitBootstrapper(this);
            defaultBootstrapper.Run();
        }
    }
}
