using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Policy;
using System.Text;

namespace Misfit.Domain
{
    /// <summary>
    /// 插件域工厂
    /// </summary>
    public class PluginDomainFactory
    {

        /// <summary>
        /// 创建新的插件的应用域
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static AppDomain CreatePluginAppDomain(string domainName, string applicationBase)
        {
            return CreateAppDomain("plugin-" + domainName, applicationBase, string.Empty);
        }

        /// <summary>
        /// 创建新的应用域
        /// </summary>
        /// <param name="domainName"></param>
        /// <param name="applicationBase"></param>
        /// <param name="privateBinPath"></param>
        /// <returns></returns>
        public static AppDomain CreateAppDomain(string domainName, string applicationBase, string privateBinPath)
        {
            AppDomainSetup info = new AppDomainSetup();
            info.ApplicationName = domainName;
            info.ApplicationBase = applicationBase;

            if (string.IsNullOrWhiteSpace(privateBinPath))
            {
                info.DisallowApplicationBaseProbing = false;
                info.PrivateBinPath = privateBinPath;
            }

            info.ShadowCopyDirectories = Path.Combine(info.ApplicationBase, @"cache");
            info.ShadowCopyFiles = "true";
            Evidence baseEvidence = AppDomain.CurrentDomain.Evidence;
            Evidence evidence = new Evidence(baseEvidence);
            return AppDomain.CreateDomain(domainName, evidence, info);
        }
    }
}
