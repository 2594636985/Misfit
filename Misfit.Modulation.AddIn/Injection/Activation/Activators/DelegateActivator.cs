using Misfit.Modulation.AddIn.Injection;
using System;

namespace Misfit.Modulation.AddIn.Injection.Activation.Activators
{
    /// <summary>
    /// 委托激活
    /// </summary>
    internal class DelegateActivator : IActivator
    {
        private readonly Func<IMisfitContainer, object> _activationFunction;
       
        internal DelegateActivator(Func<IMisfitContainer, object> activationFunction)
        {
            this._activationFunction = activationFunction;
        }

        public object ActivateInstance(ResolutionContext resolutionContext)
        {
            return this._activationFunction(resolutionContext.Container);
        }
    }
}