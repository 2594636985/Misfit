using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Misfit.AddIn.Injection
{
    /// <summary>
    /// 匿名参数
    /// </summary>
    public class AnonymousArgument
    {
        public object AnonymousType { get; set; }
        public AnonymousArgument(object anonymousNamedArguments)
        {
            this.AnonymousType = anonymousNamedArguments;
        }
    }
}
