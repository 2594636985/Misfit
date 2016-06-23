using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Misfit.AddIn.Injection
{
    /// <summary>
    /// Ioc异常类
    /// </summary>
    public class InjectionException : Core.CoreException
    {
        public InjectionException(string message)
            : base(message)
        {

        }
    }
}
