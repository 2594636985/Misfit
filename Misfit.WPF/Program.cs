using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Misfit.WPF
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Bootstrapper bootstrapper = new Bootstrapper();
            bootstrapper.OnExited += MisfitBootstrapper_OnExited;
            bootstrapper.Initialize();
            bootstrapper.Execute();
        }

        static void MisfitBootstrapper_OnExited(Bootstrapper obj)
        {
            throw new NotImplementedException();
        }
    }
}
