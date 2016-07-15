using Misfit.Plugins.Injection;
using Misfit.Plugins.Injection.Registration;

namespace Misfit.Plugins.Injection.Activation
{
    /// <summary>
    /// 解析的上下文
    /// </summary>
    internal class ResolutionContext
    {
        internal IPluginContainer Container { get; set; }

        internal RegistrationItem Registration { get; set; }

        internal IRegistrationContainer RegistrationContainer { get; set;}

        internal ArgumentContainer Arguments { get; set; }

        internal ArgumentContainer RuntimeArguments { get; set; }


        internal ResolutionContext()
        {
            this.Arguments = new ArgumentContainer();
            this.RuntimeArguments = new ArgumentContainer();
        }

        internal ResolutionContext(IPluginContainer container, IRegistrationContainer registrations)
            : this()
        {
            this.Container = container;
            this.RegistrationContainer = registrations;
        }
     
        internal ResolutionContext(IPluginContainer container, IRegistrationContainer registrations, ArgumentContainer arguments, ArgumentContainer runtimeArguments)
            : this(container, registrations)
        {
            this.Arguments = arguments;
            this.RuntimeArguments = runtimeArguments;
        }
    }
}