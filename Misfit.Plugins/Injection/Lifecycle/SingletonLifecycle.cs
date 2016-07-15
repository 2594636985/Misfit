using System;

namespace Misfit.Plugins.Injection.Lifecycle
{
    /// <summary>
    /// 只有一个实例的生命周期
    /// </summary>
    public class SingletonLifecycle : ILifecycle
    {
        private readonly object _lock = new object();
        private object _instance;

        public object ReceiveInstanceInLifecycle(Func<object> newInstanceResolver)
        {
            lock (_lock)
            {
                if (this._instance == null)
                {
                    this._instance = newInstanceResolver();
                }

                return this._instance;
            }
        }
    }
}