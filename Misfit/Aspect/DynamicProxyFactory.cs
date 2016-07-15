using System;

namespace Misfit.Aspect
{
    /// <summary>
    /// ��̬������
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
        /// ����һ����̬�������
        /// </summary>
        /// <param name="target"></param>
        /// <param name="invocationHandler"></param>
        /// <returns></returns>
        public object CreateProxy(object target, InvocationDelegate invocationHandler)
        {
            return CreateProxy(target, invocationHandler, false, null);
        }

        /// <summary>
        /// ����һ����̬�������
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
        /// ����һ����̬�������
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
