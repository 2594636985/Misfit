using System;
using System.Diagnostics;

namespace Misfit.Tracker
{

    public class DefaultTrace : ITrace
    {
        public void OutputTrace(string trace)
        {
            Debug.WriteLine("---" + trace);
            System.Console.WriteLine("--" + trace);
        }
    }
}

