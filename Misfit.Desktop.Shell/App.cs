using MisfitThemes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Misfit.Modulation.Desktop.Shell
{
    public class App : MisfitApplication
    {
        public static MisfitWeaver MisfitWeaver = new MisfitWeaver();
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            MisfitWeaver.OnMisfitException += MisfitWeaver_OnMisfitException;
            MisfitWeaver.Initailize();

            MisfitBootstrapper defaultBootstrapper = new MisfitBootstrapper(this);
            defaultBootstrapper.Run();
        }

        private void MisfitWeaver_OnMisfitException(Exception ex)
        {
           
        }


        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            MisfitWeaver.Dispose();
        }
    }
}
