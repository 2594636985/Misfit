using System;

namespace Misfit.AddIn.Injection.Lifecycle
{
    /// <summary>
    /// ������������
    /// </summary>
    public interface ILifecycle
    {
        object ReceiveInstanceInLifecycle(Func<object> newInstanceResolver);
    }
}