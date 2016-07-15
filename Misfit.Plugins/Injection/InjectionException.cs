using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Misfit.Plugins.Injection
{
    /// <summary>
    /// Ioc异常类
    /// </summary>
    public class InjectionException : PluginException
    {
        public InjectionException(string message)
            : base(message)
        {

        }
    }
}
