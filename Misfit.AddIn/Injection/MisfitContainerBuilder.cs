using Misfit.AddIn.Injection.Activation.Activators;
using Misfit.AddIn.Injection.Activation.Components;
using Misfit.AddIn.Injection.Lifecycle;
using Misfit.AddIn.Injection.Registration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Misfit.AddIn.Injection
{
    /// <summary>
    /// 用于建设容器的类
    /// </summary>
    public class MisfitContainerBuilder : IMisfitContainerBuilder
    {
        private readonly IMisfitContainer _bootStrappingContainer;
        private readonly IRegistrationContainer _registrationContainer;
        private readonly IList<Action> _registrationCallbacks;

        public MisfitContainerBuilder()
        {
            this._registrationContainer = new RegistrationContainer();
            this._registrationCallbacks = new List<Action>();
            this._bootStrappingContainer = this.Build();
        }

        public IMisfitContainer Build()
        {
            Type typeOfArgumentCollector = typeof(IArgumentCollector);
            Type typeOfConstructorSelector = typeof(IConstructorSelector);

            if (!this._registrationContainer.IsRegistered(typeOfArgumentCollector))
            {
                this.Register<IArgumentCollector>(c => new ArgumentCollector()).SingletonLifecycle();
            }

            if (!this._registrationContainer.IsRegistered(typeOfConstructorSelector))
            {
                this.Register<IConstructorSelector>(c => new ConstructorSelector()).SingletonLifecycle();
            }

            this._registrationCallbacks.ForEach(registerCallback => registerCallback());
            this._registrationCallbacks.Clear();

            var container = new MisfitContainer(this._registrationContainer);

            return container;
        }
        /// <summary>
        /// 通过一个实例来注册
        /// </summary>
        /// <typeparam name="TInstance"></typeparam>
        /// <param name="instance"></param>
        /// <returns></returns>
        public IRegistrationBuilder Register<TInstance>(TInstance instance)
        {
            return this.AddToRegistrationBuilder(new RegistrationItem(typeof(TInstance))
            {
                Activator = new InstanceActivator<TInstance>(instance)
            });
        }

        public IRegistrationBuilder Register<TContract>(Func<IMisfitContainer, TContract> activatorFunction)
        {
            return this.AddToRegistrationBuilder(new RegistrationItem(typeof(TContract))
            {
                Activator = new DelegateActivator(c => activatorFunction(c))
            });
        }


        private IRegistrationBuilder AddToRegistrationBuilder(RegistrationItem registrationItem)
        {
            Action registrationCallback = () =>
            {
                if (registrationItem.Lifecycle == null)
                    registrationItem.Lifecycle = new TransientLifecycle();

                this._registrationContainer.AddRegistration(registrationItem);
            };

            this._registrationCallbacks.Add(registrationCallback);

            return new RegistrationBuilder(registrationItem);
        }

        /// <summary>
        /// 通过实体类和接口类来注册
        /// </summary>
        /// <typeparam name="TContract"></typeparam>
        /// <typeparam name="TImplementation"></typeparam>
        /// <returns></returns>
        public IRegistrationBuilder Register<TContract, TImplementation>() where TImplementation : TContract
        {
            return this.Register(typeof(TContract), typeof(TImplementation));
        }

        /// <summary>
        /// 通过实体类和接口类的TYPE来注册
        /// </summary>
        /// <param name="typeOfContract"></param>
        /// <param name="typeOfImplementation"></param>
        /// <returns></returns>
        public IRegistrationBuilder Register(Type typeOfContract, Type typeOfImplementation)
        {
            if (!typeOfContract.IsGenericTypeDefinition && !typeOfContract.IsAssignableFrom(typeOfImplementation))
                throw new InjectionException(string.Format("{0} 没有继承对应的{1}", typeOfContract.Name, typeOfImplementation.Name));

            return this.AddToRegistrationBuilder(new RegistrationItem(typeOfContract)
            {
                Activator = new ReflectionActivator(
                    typeOfImplementation,
                    this._bootStrappingContainer.Resolve<IConstructorSelector>(),
                    this._bootStrappingContainer.Resolve<IArgumentCollector>()),
                ImplementationType = typeOfImplementation
            });
        }
    }
}
