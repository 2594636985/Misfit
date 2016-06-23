using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Misfit.Desktop.Shell
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {

            //App app = new App();
            //app.Run();

            MisfitWeaver.Initailize();

            MisfitWeaver.Dispose();
        }
    }
}
