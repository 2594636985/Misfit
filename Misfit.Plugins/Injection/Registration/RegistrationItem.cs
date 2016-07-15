using Misfit.Plugins.Injection.Activation;
using Misfit.Plugins.Injection.Activation.Activators;
using Misfit.Plugins.Injection.Lifecycle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Misfit.Plugins.Injection.Registration
{
    /// <summary>
    /// 注册对象信息
    /// </summary>
    public class RegistrationItem
    {
        internal Type ContractType { get; private set; }
        internal Type ImplementationType { get; set; }
        internal IActivator Activator { get; set; }

        internal ILifecycle Lifecycle { get; set; }

        internal ArgumentContainer Arguments { get; set; }

        internal ArgumentContainer RuntimeArguments { get; set; }

        internal RegistrationItem()
        {
            this.Arguments = new ArgumentContainer();
            this.RuntimeArguments = new ArgumentContainer();
        }

        internal RegistrationItem(Type contractType)
            : this()
        {
            this.ContractType = contractType;

            if (this.ImplementationType == null)
            {
                this.ImplementationType = contractType;
            }
        }


        internal object ActivateInstance(ResolutionContext resolutionContext)
        {
            return this.Lifecycle.ReceiveInstanceInLifecycle(
                () => this.Activator.ActivateInstance(resolutionContext));
        }


        public override bool Equals(object obj)
        {
            return Equals(this, obj as RegistrationItem);
        }

        public static bool Equals(RegistrationItem obj1, RegistrationItem obj2)
        {
            if ((object.Equals(null, obj1) || object.Equals(null, obj2)) || (obj1.GetType() != obj2.GetType()))
            {
                return false;
            }

            return obj1.ContractType == obj2.ContractType
                   && obj1.ImplementationType == obj2.ImplementationType
                   && ReferenceEquals(obj1, obj2);
        }

        public override int GetHashCode()
        {
            return this.ContractType.GetHashCode() ^ this.ImplementationType.GetHashCode();
        }
    }
}
