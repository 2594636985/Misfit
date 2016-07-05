using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Misfit.Configuration.Elements
{
    /// <summary>
    /// 模块付带的参数元素集合类
    /// </summary>
    public class ParameterElementCollection : KeyConfigurationElementCollection<ParameterElement>
    {
        public ParameterElementCollection()
            : base("Parameter", ParameterElement.KeyAttributeName)
        {

        }
    }
}
