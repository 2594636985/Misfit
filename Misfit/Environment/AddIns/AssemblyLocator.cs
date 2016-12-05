using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Misfit.Environment.AddIns
{
    public class AssemblyLocator
    {
        private static Dictionary<string, AssemblyInfo> preAssemblies = new Dictionary<string, AssemblyInfo>();
        private static Dictionary<string, Assembly> assembies = new Dictionary<string, Assembly>();
        private static bool initialized;

        public static void Initialize()
        {
            if (!initialized)
            {
                AppDomain.CurrentDomain.AssemblyLoad += CurrentDomain_AssemblyLoad;
                AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
                initialized = true;
            }

        }

        public static void AddAssebmlyInfo(AssemblyInfo assemblyInfo)
        {
            preAssemblies.Add(assemblyInfo.Name, assemblyInfo);
        }


        static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            throw new NotImplementedException();
        }

        static void CurrentDomain_AssemblyLoad(object sender, AssemblyLoadEventArgs args)
        {
            throw new NotImplementedException();
        }
    }
}
