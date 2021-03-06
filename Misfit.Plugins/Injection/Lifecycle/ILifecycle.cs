using System;

namespace Misfit.Plugins.Injection.Lifecycle
{
    /// <summary>
    /// 对象生命周期
    /// </summary>
    public interface ILifecycle
    {
        object ReceiveInstanceInLifecycle(Func<object> newInstanceResolver);
    }
}