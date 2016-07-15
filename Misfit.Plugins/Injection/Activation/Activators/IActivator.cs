namespace Misfit.Plugins.Injection.Activation.Activators
{
    internal interface IActivator
    {
        object ActivateInstance(ResolutionContext resolutionContext);
    }
}