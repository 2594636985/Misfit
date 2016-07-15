using System;
using System.Reflection;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;

namespace Misfit.Aspect
{
    /// <summary>
    /// 动态代理
    /// </summary>
    public class DynamicProxyImpl : RealProxy, IDynamicProxy, IRemotingTypeInfo
    {
        /// <summary>
        /// 动态代理对象
        /// </summary>
        public object ProxyTarget { set; get; }

        /// <summary>
        /// 处理事件
        /// </summary>
        public InvocationDelegate InvocationHandler { set; get; }

        /// <summary>
        /// 是否验检是否可以转化
        /// </summary>
        public bool Strict { set; get; }

        /// <summary>
        /// 支持的类型
        /// </summary>
        public Type[] SupportedTypes { set; get; }

        protected internal DynamicProxyImpl(object proxyTarget, InvocationDelegate invocationHandler, bool strict, Type[] supportedTypes)
            : base(typeof(IDynamicProxy))
        {
            this.ProxyTarget = proxyTarget;
            this.InvocationHandler = invocationHandler;
            this.Strict = strict;
            this.SupportedTypes = supportedTypes;
        }

        /// <summary>
        /// 用于验检是否可以转化成对应的对象
        /// </summary>
        /// <param name="toType"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool CanCastTo(System.Type toType, object obj)
        {
            bool canCast = true;

            if (this.Strict)
            {
                if (toType.IsAssignableFrom(this.ProxyTarget.GetType()))
                {
                    canCast = true;
                }
                else if (this.SupportedTypes != null)
                {
                    canCast = false;

                    foreach (Type type in this.SupportedTypes)
                    {
                        if (toType == type)
                        {
                            canCast = true;
                            break;
                        }
                    }
                }
                else
                {
                    canCast = false;
                }
            }

            return canCast;
        }


        /// <summary>
        /// 当你代理的对象执行方法的时候，都会调用这个方法
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public override IMessage Invoke(IMessage message)
        {
            IMethodCallMessage methodMessage = new MethodCallMessageWrapper((IMethodCallMessage)message);

            MethodBase method = methodMessage.MethodBase;

            object returnValue = null;

            if (method.DeclaringType == typeof(IDynamicProxy))
            {
                returnValue = method.Invoke(this, methodMessage.Args);
            }
            else
            {
                returnValue = this.InvocationHandler(this.ProxyTarget, method, methodMessage.Args);
            }

            ReturnMessage returnMessage = new ReturnMessage(returnValue, methodMessage.Args, methodMessage.ArgCount, methodMessage.LogicalCallContext, methodMessage);
            return returnMessage;
        }

        public string TypeName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
    }
}
