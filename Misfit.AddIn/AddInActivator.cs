using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;

namespace Misfit.AddIn
{
    [Serializable]
    public abstract class AddInActivator : MarshalByRefObject, IPluginActivator
    {
        /// <summary>
        /// 开始
        /// </summary>
        /// <param name="context"></param>
        public abstract void Start(string[] args);

        /// <summary>
        /// 结束
        /// </summary>
        /// <param name="context"></param>
        public abstract void Stop(string[] args);

    }
}
