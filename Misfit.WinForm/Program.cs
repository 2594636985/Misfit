using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Misfit.WinForm
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Bootstrapper bootstrapper = new Bootstrapper();
            bootstrapper.OnExited += MisfitBootstrapper_OnExited;
            bootstrapper.Initialize();
        }

        static void MisfitBootstrapper_OnExited(Bootstrapper obj)
        {
            throw new NotImplementedException();
        }
    }
}
