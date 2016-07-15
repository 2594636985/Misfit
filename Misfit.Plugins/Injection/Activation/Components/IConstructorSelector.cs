using System.Collections.Generic;
using System.Reflection;

namespace Misfit.Plugins.Injection.Activation.Components
{
    /// <summary>
    /// 构选函数的选择器
    /// </summary>
    internal interface IConstructorSelector
    {
        ConstructorInfo SelectConstructor(IEnumerable<ConstructorInfo> constructors, ResolutionContext resolutionContext);
    }
}