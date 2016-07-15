using System;

namespace Misfit.Plugins.Injection.Lifecycle
{
    /// <summary>
    /// 每一次都创建一个新的对象的生命周期
    /// </summary>
    public class TransientLifecycle : ILifecycle
    {
        public object ReceiveInstanceInLifecycle(Func<object> newInstanceResolver)
        {
            return newInstanceResolver();
        }
    }
}