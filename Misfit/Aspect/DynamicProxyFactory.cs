using System;

namespace Misfit.Aspect
{
    /// <summary>
    /// 动态代理工厂
    /// </summary>
    public class DynamicProxyFactory
    {
        private static DynamicProxyFactory self = new DynamicProxyFactory();

        private DynamicProxyFactory()
        {
        }
        public static DynamicProxyFactory Instance
        {
            get { return self; }
        }

        /// <summary>
        /// 创建一个动态代理对象
        /// </summary>
        /// <param name="target"></param>
        /// <param name="invocationHandler"></param>
        /// <returns></returns>
        public object CreateProxy(object target, InvocationDelegate invocationHandler)
        {
            return CreateProxy(target, invocationHandler, false, null);
        }

        /// <summary>
        /// 创建一个动态代理对象
        /// </summary>
        /// <param name="target"></param>
        /// <param name="invocationHandler"></param>
        /// <param name="strict"></param>
        /// <returns></returns>
        public object CreateProxy(object target, InvocationDelegate invocationHandler, bool strict)
        {
            return CreateProxy(target, invocationHandler, strict, null);
        }

        /// <summary>
        /// 创建一个动态代理对象
        /// </summary>
        /// <param name="target"></param>
        /// <param name="invocationHandler"></param>
        /// <param name="strict"></param>
        /// <param name="supportedTypes"></param>
        /// <returns></returns>
        public object CreateProxy(object target, InvocationDelegate invocationHandler, bool strict, Type[] supportedTypes)
        {
            return new DynamicProxyImpl(target, invocationHandler, strict, supportedTypes).GetTransparentProxy();
        }
    }
}
