using System;
using System.Diagnostics;

namespace Misfit.AddIn.Services.Impl
{
    /// <summary>
    /// Standard output to the console.
    /// </summary>
    public class DefaultTrace : ITrace
    {
        public void OutputTrace(string trace)
        {
            Debug.WriteLine("---" + trace);
            Console.WriteLine("--" + trace);
        }
    }
}

