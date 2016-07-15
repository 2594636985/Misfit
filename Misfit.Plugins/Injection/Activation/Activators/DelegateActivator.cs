using Misfit.Plugins.Injection;
using System;

namespace Misfit.Plugins.Injection.Activation.Activators
{
    /// <summary>
    /// 委托激活
    /// </summary>
    internal class DelegateActivator : IActivator
    {
        private readonly Func<IPluginContainer, object> _activationFunction;
       
        internal DelegateActivator(Func<IPluginContainer, object> activationFunction)
        {
            this._activationFunction = activationFunction;
        }

        public object ActivateInstance(ResolutionContext resolutionContext)
        {
            return this._activationFunction(resolutionContext.Container);
        }
    }
}