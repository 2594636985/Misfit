using Misfit.Plugins.Injection.Lifecycle;
using System;
using System.Collections.Generic;

namespace Misfit.Plugins.Injection.Registration
{
    /// <summary>
    /// 注册建立类  用于注册的时候之后的相关功能操作
    /// </summary>
    internal class RegistrationBuilder : IRegistrationBuilder
    {
        private readonly RegistrationItem _registrationItem;
        internal RegistrationBuilder(RegistrationItem registrationItem)
        {
            this._registrationItem = registrationItem;
        }

        public IRegistrationBuilder TransientLifecycle()
        {
            this._registrationItem.Lifecycle = new TransientLifecycle();
            return this;
        }
        public IRegistrationBuilder SingletonLifecycle()
        {
            this._registrationItem.Lifecycle = new SingletonLifecycle();
            return this;
        }

        public IRegistrationBuilder WithArguments(params object[] arguments)
        {
            this._registrationItem.Arguments.AddToAnonymousArguments(arguments);
            return this;
        }


        public IRegistrationBuilder WithNamedArguments(object namedArguments)
        {
            return this.WithNamedArguments(namedArguments.ToNamedArgumentDictionary());
        }

        public IRegistrationBuilder WithNamedArguments(IDictionary<string, object> namedArguments)
        {
            this._registrationItem.Arguments.AddToNamedArguments(namedArguments);
            return this;
        }
    }
}