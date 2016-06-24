using Misfit.Modulation.AddIn.Injection.Lifecycle;
using System;
using System.Collections.Generic;

namespace Misfit.Modulation.AddIn.Injection.Registration
{
    /// <summary>
    /// 注册建立类  用于注册的时候之后的相关功能操作
    /// </summary>
    public interface IRegistrationBuilder
    {
        /// <summary>
        /// 每一次都实例化一个新的对象
        /// </summary>
        /// <returns></returns>
        IRegistrationBuilder TransientLifecycle();
        /// <summary>
        /// 从头到尾都是同个实例
        /// </summary>
        /// <returns></returns>
        IRegistrationBuilder SingletonLifecycle();

        /// <summary>
        /// 注册的时候，给定的参数
        /// </summary>
        /// <param name="arguments"></param>
        /// <returns></returns>
        IRegistrationBuilder WithArguments(params object[] arguments);

        /// <summary>
        /// 注册的时候，给定的参数
        /// </summary>
        /// <param name="namedArguments"></param>
        /// <returns></returns>
        IRegistrationBuilder WithNamedArguments(object namedArguments);

        /// <summary>
        /// 注册的时候，给定的参数
        /// </summary>
        /// <param name="namedArguments"></param>
        /// <returns></returns>
        IRegistrationBuilder WithNamedArguments(IDictionary<string, object> namedArguments);

    }
}