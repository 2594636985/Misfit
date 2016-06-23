using System.Collections.Generic;
using System.Reflection;

namespace Misfit.AddIn.Injection.Activation.Components
{
    /// <summary>
    /// ��ѡ������ѡ����
    /// </summary>
    internal interface IConstructorSelector
    {
        ConstructorInfo SelectConstructor(IEnumerable<ConstructorInfo> constructors, ResolutionContext resolutionContext);
    }
}