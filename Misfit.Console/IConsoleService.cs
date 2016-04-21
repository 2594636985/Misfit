using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Misfit.Console
{
    public interface IConsoleService
    {
        void Initialize();
        void Start();
        void Stop();
        string ReadLine();
        void WriteLine(object value);
        void DoCommand(string commandLine);
    }
}
