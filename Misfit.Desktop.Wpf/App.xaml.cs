using Misfit.Modulation;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;

namespace Misfit.Desktop.Wpf
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        public static MisfitWeaver MisfitWeaver = new MisfitWeaver();
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            MisfitWeaver.OnMisfitException += MisfitWeaver_OnMisfitException;
            MisfitWeaver.Initailize();
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
