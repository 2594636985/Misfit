using System;

namespace Misfit.Modulation.AddIn.Injection.Lifecycle
{
    /// <summary>
    /// ������������
    /// </summary>
    public interface ILifecycle
    {
        object ReceiveInstanceInLifecycle(Func<object> newInstanceResolver);
    }
}