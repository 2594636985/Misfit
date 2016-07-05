using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Misfit.Configuration.Elements
{
    /// <summary>
    /// 变量元素集合类
    /// </summary>
    public class VariableElementCollection : KeyConfigurationElementCollection<VariableElement>
    {
        public VariableElementCollection()
            : base("Variable", VariableElement.NameAttributeName)
        {

        }
    }
}
