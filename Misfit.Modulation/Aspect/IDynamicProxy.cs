using System;
using System.Reflection;
using System.Runtime.Remoting;

namespace Misfit.Modulation.Aspect
{
    /// <summary>
    /// 动态代理接口
    /// </summary>
	public interface IDynamicProxy {
        /// <summary>
        /// 代理对象
        /// </summary>
		object ProxyTarget { get; set; }
        /// <summary>
        /// 方法委托处理事件
        /// </summary>
		InvocationDelegate InvocationHandler { get; set; }
        /// <summary>
        /// 是否比较严谨
        /// </summary>
		bool Strict { get;  set; }
        /// <summary>
        /// 如果Strict设置TURE的话   
        /// </summary>
		Type[] SupportedTypes {
			get;
			set;
		}
	}
}
