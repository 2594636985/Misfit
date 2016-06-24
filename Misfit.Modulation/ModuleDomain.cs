using Misfit.Modulation.AddIn;
using Misfit.Modulation.AddIn.Core;
using Misfit.Modulation.AddIn.Injection;
using Misfit.Modulation.AddIn.IO;
using Misfit.Modulation.AddIn.Serices;
using Misfit.Modulation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Misfit.Modulation
{
    /// <summary>
    /// 模块域
    /// </summary>
    public class ModuleDomain
    {
        /// <summary>
        /// 模块域的哉名
        /// </summary>
        public string DomainName { private set; get; }

        /// <summary>
        /// 模块域的名称
        /// </summary>
        public string ModuleName { private set; get; }

        /// <summary>
        /// 模块域上下文
        /// </summary>
        public ModuleDomainContext ModuleDomainContext { private set; get; }

        /// <summary>
        /// 对应的应用域
        /// </summary>

        public AppDomain Domain { private set; get; }

        /// <summary>
        /// 是否安装过
        /// </summary>
        public bool Installed { private set; get; }

        /// <summary>
        /// 模块域的对外服务
        /// </summary>
        public Dictionary<string, object> ModuleDomainServices { private set; get; }


        public ModuleDomain(ModuleDomainContext moduleDomainContext)
        {
            this.ModuleDomainContext = moduleDomainContext;
            this.DomainName = moduleDomainContext.ModuleDomainName;
            this.Installed = false;
        }

        /// <summary>
        /// 安装
        /// </summary>
        public void Install()
        {
            this.Domain = ModuleDomainFactory.CreateModuleAppDomain("Module-" + this.ModuleDomainContext.AssemlbyLocation);

            this.Domain.SetData("Location", this.ModuleDomainContext.AssemlbyLocation);
            this.Domain.SetData("ModuleDomainContext", this.ModuleDomainContext);

            this.Domain.DoCallBack(ModuleDomainInitailize.Initailize);

            this.ModuleName = this.Domain.GetData("ModuleDomainName") as string;
            this.ModuleDomainServices = this.Domain.GetData("ModuleDomainServcies") as Dictionary<string, object>;

            this.Installed = true;

        }

        /// <summary>
        /// 卸载
        /// </summary>
        public void UnInstall()
        {
            if (this.ModuleDomainServices != null)
                this.ModuleDomainServices.Clear();

            if (this.Domain != null)
                AppDomain.Unload(this.Domain);

            this.Installed = false;
        }



    }
}
