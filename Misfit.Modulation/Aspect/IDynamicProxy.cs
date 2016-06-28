using System;
using System.Reflection;
using System.Runtime.Remoting;

namespace Misfit.Modulation.Aspect
{
    /// <summary>
    /// ��̬����ӿ�
    /// </summary>
	public interface IDynamicProxy {
        /// <summary>
        /// �������
        /// </summary>
		object ProxyTarget { get; set; }
        /// <summary>
        /// ����ί�д����¼�
        /// </summary>
		InvocationDelegate InvocationHandler { get; set; }
        /// <summary>
        /// �Ƿ�Ƚ��Ͻ�
        /// </summary>
		bool Strict { get;  set; }
        /// <summary>
        /// ���Strict����TURE�Ļ�   
        /// </summary>
		Type[] SupportedTypes {
			get;
			set;
		}
	}
}
