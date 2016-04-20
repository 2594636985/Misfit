using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using Misfit.AddIn;
using Misfit.AddIn.Services;
using Misfit.AddIn.Services.Impl;

namespace Misfit.AddIn.Core
{

    /// <summary>
    /// 框架启动器类
    /// </summary>
    public class MisfitBootstrapper
    {
        private string[] _arguments;
        private Framework _framework;

        public event Action<Exception> OnException;

        public void Run(string[] args)
        {
            TracesProvider.Initialize(new DefaultTrace());

            this._arguments = args;

            this._framework = new Framework();
            this._framework.InitializeFramework();
            this._framework.Launch();
        }

        public void Close()
        {
            _framework.Close();
        }
    }
}
