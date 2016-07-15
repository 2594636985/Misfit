using Misfit.Plugins.Injection.Activation;
using Misfit.Plugins.Injection.Activation.Activators;
using Misfit.Plugins.Injection.Lifecycle;
using Misfit.Plugins.Injection.Registration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Misfit.Plugins.Injection
{
    /// <summary>
    /// 存放对象的容器
    /// </summary>
    public class MisfitContainer : IPluginContainer
    {
        private readonly IRegistrationContainer _registrationContainer;

        internal MisfitContainer(IRegistrationContainer registrationContainer)
        {
            this._registrationContainer = registrationContainer;

            var typeOfIContainer = typeof(IPluginContainer);

            if (this._registrationContainer.IsRegistered(typeOfIContainer))
            {
                this._registrationContainer.RemoveRegistration(typeOfIContainer);
            }

            var registrationItem = new RegistrationItem(typeOfIContainer)
            {
                Activator = new InstanceActivator<IPluginContainer>(this),
                Lifecycle = new TransientLifecycle()
            };

            this._registrationContainer.AddRegistration(registrationItem);
        }

        public TContract Resolve<TContract>()
        {
            return (TContract)this.Resolve(typeof(TContract));
        }


        public TContract Resolve<TContract>(params object[] arguments)
        {
            return this.Resolve<TContract>(arguments.ToList());
        }


        public TContract Resolve<TContract>(IEnumerable<object> arguments)
        {
            return (TContract)this.Resolve(typeof(TContract), arguments);
        }


        public TContract Resolve<TContract>(IDictionary<string, object> namedArguments)
        {
            return (TContract)this.Resolve(typeof(TContract), namedArguments);
        }


        public TContract Resolve<TContract>(AnonymousArgument namedArguments)
        {
            return (TContract)this.Resolve(typeof(TContract),
                                            namedArguments.AnonymousType.ToNamedArgumentDictionary());
        }


        public object Resolve(Type contractType)
        {
            return this.ResolveInternal(contractType, null, null);
        }


        private object ResolveInternal(Type contractType, IEnumerable<object> arguments,
                                       IDictionary<string, object> namedArguments)
        {
            RegistrationItem registrationItem;

            if (!this._registrationContainer.TryGetRegistration(contractType, out registrationItem))
                throw new InvalidOperationException(string.Format("{0} 没有找到合适的Registration", contractType.Name));

            if (arguments != null)
            {
                registrationItem.RuntimeArguments.AddToAnonymousArguments(arguments);
            }
            if (namedArguments != null)
            {
                registrationItem.RuntimeArguments.AddToNamedArguments(namedArguments);
            }

            var result = this.Resolve(registrationItem);

            return result;
        }

        public object Resolve(Type contractType, params object[] arguments)
        {
            return this.ResolveInternal(contractType, arguments, null);
        }


        public object Resolve(Type contractType, IEnumerable<object> arguments)
        {
            return this.ResolveInternal(contractType, arguments, null);
        }


        public object Resolve(Type contractType, IDictionary<string, object> namedArguments)
        {
            return this.ResolveInternal(contractType, null, namedArguments);
        }


        private object Resolve(RegistrationItem registrationItem)
        {
            ResolutionContext resolutionContext = new ResolutionContext()
            {
                Container = this,
                RegistrationContainer = this._registrationContainer,
                Arguments = registrationItem.Arguments,
                RuntimeArguments = registrationItem.RuntimeArguments,
                Registration = registrationItem
            };

            object instance = registrationItem.ActivateInstance(resolutionContext);

            registrationItem.RuntimeArguments.AnonymousArguments = null;
            registrationItem.RuntimeArguments.NamedArguments = null;

            return instance;
        }


        public IEnumerable<TContract> ResolveAll<TContract>()
        {
            return this.ResolveAll(typeof(TContract)).Cast<TContract>();
        }

        /// <summary>
        /// 解释出所有的对象实例
        /// </summary>
        /// <returns></returns>
        public IEnumerable<object> ResolveAll()
        {
            return this._registrationContainer
                .AllRegistrations
                .Select<RegistrationItem, object>(this.Resolve);
        }

        /// <summary>
        /// 根据给定的类型解释出对应下的所有对象实例
        /// </summary>
        /// <param name="contractType"></param>
        /// <returns></returns>
        public IEnumerable<object> ResolveAll(Type contractType)
        {
            return this._registrationContainer.AllRegistrations
                .Where(r => r.ContractType == contractType)
                .Select<RegistrationItem, object>(this.Resolve);
        }

        /// <summary>
        /// 判断是否注册过
        /// </summary>
        /// <param name="contractType"></param>
        /// <returns></returns>
        public bool HasRegistration(Type contractType)
        {
            return this._registrationContainer.IsRegistered(contractType);
        }

        /// <summary>
        /// 判断是否注册过
        /// </summary>
        /// <typeparam name="TContract"></typeparam>
        /// <returns></returns>
        public bool HasRegistration<TContract>()
        {
            return this.HasRegistration(typeof(TContract));
        }

    }
}
