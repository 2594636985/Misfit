using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;

namespace Misfit.Core
{
    public class ModuleDomainFactory
    {
        /// <summary>
        /// 创建新的应用域
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static AppDomain CreateDomain(string domainName)
        {
            AppDomainSetup info = new AppDomainSetup();
            info.ApplicationName = domainName;
            info.ApplicationBase = AppDomain.CurrentDomain.BaseDirectory;
            info.DisallowApplicationBaseProbing = false;
            info.PrivateBinPath = Constants.AddInsFileRoot;
            info.ShadowCopyDirectories = Path.Combine(info.ApplicationBase, @"cache");
            info.ShadowCopyFiles = "true";
            Evidence baseEvidence = AppDomain.CurrentDomain.Evidence;
            Evidence evidence = new Evidence(baseEvidence);
            return AppDomain.CreateDomain(domainName, evidence, info);
        }
    }
}
