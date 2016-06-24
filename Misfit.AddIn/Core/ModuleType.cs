using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Misfit.Modulation.AddIn.Core
{
    /// <summary>
    /// 模块中的对外类型信息
    /// </summary>
    public class ModuleServiceType
    {
        public ModuleServiceType()
        { }

        public ModuleServiceType(Type interfaceType, Type implementType)
        {
            this.InterfaceType = interfaceType;
            this.ImplementType = implementType;
        }

        public Type InterfaceType { set; get; }

        public Type ImplementType { set; get; }
    }
}
