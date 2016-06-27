using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Policy;
using System.Text;

namespace Misfit.Modulation
{
    /// <summary>
    /// 模块域工厂
    /// </summary>
    public class ModuleDomainFactory
    {
        /// <summary>
        /// 创建新的模块的应用域
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static AppDomain CreateModuleAppDomain(string domainName)
        {
            return CreateAppDomain(domainName, Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Constants.AddInsFileRoot), string.Empty);
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
