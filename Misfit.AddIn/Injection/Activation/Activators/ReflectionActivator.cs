using Misfit.Modulation.AddIn.Injection.Activation.Components;
using System;
using System.Reflection;

namespace Misfit.Modulation.AddIn.Injection.Activation.Activators
{
    /// <summary>
    /// 用反射来实例
    /// </summary>
    internal class ReflectionActivator : IActivator
    {
        private IMisfitContainer _container;
        private readonly IConstructorSelector _constructorSelector;
        private readonly IArgumentCollector _argumentCollector;
        private readonly Type _implementationType;

        internal ReflectionActivator(Type implementationType, IConstructorSelector constructorSelector, IArgumentCollector argumentCollector)
        {
            this._implementationType = implementationType;
            this._constructorSelector = constructorSelector;
            this._argumentCollector = argumentCollector;
        }

        public object ActivateInstance(ResolutionContext resolutionContext)
        {
            if (this._container == null)
            {
                this._container = resolutionContext.Container;
            }

            var constructors = this._implementationType.GetConstructors(BindingFlags.Public | BindingFlags.Instance);
            var finalConstructor = this._constructorSelector.SelectConstructor(constructors, resolutionContext);

            object[] finalArguments = this._argumentCollector.CollectArguments(
                this._container.Resolve,
                finalConstructor.GetParameters(),
                resolutionContext);

            if (finalArguments != null && finalArguments.Length != finalConstructor.GetParameters().Length)
                throw new InvalidOperationException(string.Format("没有找到{0}对应结构函数", this._implementationType.Name));

            return finalConstructor.Invoke(finalArguments);
        }
    }
}