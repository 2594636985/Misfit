using System;

namespace Misfit.Plugins.Injection.Lifecycle
{
    /// <summary>
    /// ������������
    /// </summary>
    public interface ILifecycle
    {
        object ReceiveInstanceInLifecycle(Func<object> newInstanceResolver);
    }
}