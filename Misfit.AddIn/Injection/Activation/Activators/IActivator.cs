namespace Misfit.Modulation.AddIn.Injection.Activation.Activators
{
    internal interface IActivator
    {
        object ActivateInstance(ResolutionContext resolutionContext);
    }
}