using System;
using System.Reflection;

namespace Misfit.Modulation.Aspect
{
    /// <summary>
    /// 处理代理对象的方法委托处理事件
    /// </summary>
    /// <param name="target"></param>
    /// <param name="method"></param>
    /// <param name="parameters"></param>
    /// <returns></returns>
    public delegate object InvocationDelegate(object target, MethodBase method, object[] parameters);
}
