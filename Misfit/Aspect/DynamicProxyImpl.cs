using System;
using System.Reflection;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;

namespace Misfit.Aspect
{
    /// <summary>
    /// ��̬����
    /// </summary>
    public class DynamicProxyImpl : RealProxy, IDynamicProxy, IRemotingTypeInfo
    {
        /// <summary>
        /// ��̬�������
        /// </summary>
        public object ProxyTarget { set; get; }

        /// <summary>
        /// �����¼�
        /// </summary>
        public InvocationDelegate InvocationHandler { set; get; }

        /// <summary>
        /// �Ƿ�����Ƿ����ת��
        /// </summary>
        public bool Strict { set; get; }

        /// <summary>
        /// ֧�ֵ�����
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
        /// ��������Ƿ����ת���ɶ�Ӧ�Ķ���
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
        /// �������Ķ���ִ�з�����ʱ�򣬶�������������
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
