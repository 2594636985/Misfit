using Misfit.Modulation.AddIn.Injection;
using Misfit.Modulation.AddIn.Injection.Registration;

namespace Misfit.Modulation.AddIn.Injection.Activation
{
    /// <summary>
    /// 解析的上下文
    /// </summary>
    internal class ResolutionContext
    {
        internal IMisfitContainer Container { get; set; }

        internal RegistrationItem Registration { get; set; }

        internal IRegistrationContainer RegistrationContainer { get; set;}

        internal ArgumentContainer Arguments { get; set; }

        internal ArgumentContainer RuntimeArguments { get; set; }


        internal ResolutionContext()
        {
            this.Arguments = new ArgumentContainer();
            this.RuntimeArguments = new ArgumentContainer();
        }

        internal ResolutionContext(IMisfitContainer container, IRegistrationContainer registrations)
            : this()
        {
            this.Container = container;
            this.RegistrationContainer = registrations;
        }
     
        internal ResolutionContext(IMisfitContainer container, IRegistrationContainer registrations, ArgumentContainer arguments, ArgumentContainer runtimeArguments)
            : this(container, registrations)
        {
            this.Arguments = arguments;
            this.RuntimeArguments = runtimeArguments;
        }
    }
}