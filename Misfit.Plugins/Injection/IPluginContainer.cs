using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Misfit.Plugins.Injection
{
    /// <summary>
    /// 存在服务的注册容器
    /// </summary>
    public interface IPluginContainer
    {
        /// <summary>
        /// 根据给定的类型解释出对应的对象实例
        /// </summary>
        /// <typeparam name="TContract"></typeparam>
        /// <returns></returns>
        TContract Resolve<TContract>();

        /// <summary>
        /// 根据给定的类型解释出对应的对象实例
        /// </summary>
        /// <typeparam name="TContract"></typeparam>
        /// <param name="arguments"></param>
        /// <returns></returns>
        TContract Resolve<TContract>(params object[] arguments);

        /// <summary>
        /// 根据给定的类型解释出对应的对象实例
        /// </summary>
        /// <typeparam name="TContract"></typeparam>
        /// <param name="arguments"></param>
        /// <returns></returns>
        TContract Resolve<TContract>(IEnumerable<object> arguments);
        /// <summary>
        /// 根据给定的类型解释出对应的对象实例
        /// </summary>
        /// <typeparam name="TContract"></typeparam>
        /// <param name="namedArguments"></param>
        /// <returns></returns>
        TContract Resolve<TContract>(IDictionary<string, object> namedArguments);

        /// <summary>
        /// 根据给定的类型解释出对应的对象实例
        /// </summary>
        /// <typeparam name="TContract"></typeparam>
        /// <param name="namedArguments"></param>
        /// <returns></returns>
        TContract Resolve<TContract>(AnonymousArgument namedArguments);

        /// <summary>
        /// 根据给定的类型解释出对应的对象实例
        /// </summary>
        /// <param name="contractType"></param>
        /// <returns></returns>
        object Resolve(Type contractType);

        /// <summary>
        /// 根据给定的类型解释出对应的对象实例
        /// </summary>
        /// <param name="contractType"></param>
        /// <param name="arguments"></param>
        /// <returns></returns>
        object Resolve(Type contractType, params object[] arguments);

        /// <summary>
        /// 根据给定的类型解释出对应的对象实例
        /// </summary>
        /// <param name="contractType"></param>
        /// <param name="arguments"></param>
        /// <returns></returns>
        object Resolve(Type contractType, IEnumerable<object> arguments);

        /// <summary>
        /// 根据给定的类型解释出对应的对象实例
        /// </summary>
        /// <param name="contractType"></param>
        /// <param name="namedArguments"></param>
        /// <returns></returns>
        object Resolve(Type contractType, IDictionary<string, object> namedArguments);

        /// <summary>
        /// 解释出所有的对象实例
        /// </summary>
        /// <typeparam name="TContract"></typeparam>
        /// <returns></returns>
        IEnumerable<TContract> ResolveAll<TContract>();

        /// <summary>
        /// 根据给定的类型解释出对应下的所有对象实例
        /// </summary>
        /// <param name="contractType"></param>
        /// <returns></returns>
        IEnumerable<object> ResolveAll(Type contractType);

        /// <summary>
        /// 解释出所有的对象实例
        /// </summary>
        /// <returns></returns>
        IEnumerable<object> ResolveAll();

        /// <summary>
        /// 判断是否注册过
        /// </summary>
        /// <param name="contractType"></param>
        /// <returns></returns>
        bool HasRegistration(Type contractType);

        /// <summary>
        /// 判断是否注册过
        /// </summary>
        /// <typeparam name="TContract"></typeparam>
        /// <returns></returns>
        bool HasRegistration<TContract>();
    }
}
