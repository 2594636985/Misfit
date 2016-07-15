namespace Misfit.Plugins.Injection.Activation.Activators
{

    internal class InstanceActivator<TContract> : IActivator
    {
        private readonly TContract _instance;

        internal InstanceActivator(TContract instance)
        {
            this._instance = instance;
        }

        public object ActivateInstance(ResolutionContext resolutionContext)
        {
            return this._instance;
        }
    }
}