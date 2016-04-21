using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.Remoting;
using System.Xml;
using System.Xml.Serialization;
using Cramon.NetExtension.DynamicProxy;
using Misfit.AddIn;
using Misfit.Reflection;
using Misfit.Tracker;
using Misfit.Xml;

namespace Misfit.Core
{
    /// <summary>
    /// 每个Bundle的信息
    /// </summary>
    public class Plugin : IPlugin
    {
        private AppDomain domain;

        #region 公共属性


        /// <summary>
        /// 模块ID
        /// </summary>
        public int ModuleID { set; get; }

        public IPluginFramework PluginFramework { set; get; }

        public string Name { set; get; }

        public int Level { set; get; }

        public string Description { set; get; }

        public Version Version { set; get; }

        public string SymbolicName { set; get; }

        /// <summary>
        /// 模块状态
        /// </summary>
        public PluginState PluginState { set; get; }

        /// <summary>
        /// 插件对应的程序集
        /// </summary>
        public Assembly Assembly { private set; get; }

        /// <summary>
        /// 插件上下文
        /// </summary>
        public IPluginContext BundleContext { private set; get; }

        public IPluginActivator[] Acitvators { private set; get; }

        /// <summary>
        /// 插件物理位置
        /// </summary>
        public string Location { private set; get; }

        #endregion


        public Plugin(string name, string symbolicName, string description, int level, Version version)
        {
            this.Name = name;
            this.Description = description;
            this.Level = level;
            this.Version = version;
            this.SymbolicName = symbolicName;
            this.PluginState = PluginState.Installed;
        }

        private static object InvocationHandler(object target, MethodBase method, object[] parameters)
        {
            Debug.WriteLine("Before: " + method.Name);

            object result = method.Invoke(target, parameters);

            Debug.WriteLine("After: " + method.Name);

            return result;
        }

        /// <summary>
        /// 开始运行插件
        /// </summary>
        public virtual void Start()
        {
            try
            {
                domain = this.PluginFramework.CreateDomain(this.BundleContext);
                AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(AssemblyResolve);
                this.Assembly = Assembly.LoadFrom(this.Location);

                if (this.Acitvators == null)
                {
                    AttributeInfo[] attributes = ReflectionUtil.GetCustomAttributes(this.Assembly, typeof(AddInAttribute));
                    if (attributes != null && attributes.Length > 0)
                    {
                        HashSet<IPluginActivator> activatorSet = new HashSet<IPluginActivator>();
                        foreach (AttributeInfo attribute in attributes)
                        {
                            string typeName = attribute.Owner.FullName;
                            object obj = domain.CreateInstanceFromAndUnwrap(this.Location, typeName);
                            IPluginActivator proxy = (IPluginActivator)DynamicProxyFactory.Instance.CreateProxy(obj, new InvocationDelegate(InvocationHandler));
                            activatorSet.Add(proxy);
                        }

                        if (activatorSet.Count <= 0)
                            throw new PluginException("没有找到对应的IBundleActivator类。");

                        this.Acitvators = new IPluginActivator[activatorSet.Count];
                        activatorSet.CopyTo(this.Acitvators);
                    }
                }

                foreach (IPluginActivator activator in this.Acitvators)
                {
                    if (activator == null)
                    {
                        throw new PluginException("No activator for: " + this.Location);
                    }

                    activator.Start(this.BundleContext);
                }

                this.PluginState = PluginState.Actived;

            }
            catch (Exception ex)
            {
                this.PluginState = PluginState.Installed;
                throw new PluginException(ex.Message, ex);
            }
        }

        private static string SearchAssembly(string assemblyName)
        {
            string appRoot = AppDomain.CurrentDomain.BaseDirectory;
            string addInsRoot = Path.Combine(appRoot, Constants.AddInsFileRoot);

            {
                string[] files = Directory.GetFiles(appRoot,
                    assemblyName + ".dll", SearchOption.TopDirectoryOnly);

                if (files != null && files.Length > 0)
                {
                    return files[0];
                }
            }

            {
                string[] files = Directory.GetFiles(addInsRoot,
                    assemblyName + ".dll", SearchOption.TopDirectoryOnly);

                if (files != null && files.Length > 0)
                {
                    return files[0];
                }
            }

            return string.Empty;
        }

        private static Assembly AssemblyResolve(object sender, ResolveEventArgs args)
        {
            AssemblyName name = new AssemblyName(args.Name);
            string assemblyFile = SearchAssembly(name.Name);
            return Assembly.LoadFrom(assemblyFile);
        }

        public virtual void Stop()
        {
            try
            {
                this.PluginState = PluginState.Stopped;

                foreach (IPluginActivator activator in this.Acitvators)
                {
                    if (activator == null)
                    {
                        throw new Exception("没有找到对应的 IBundleActivator" + this.Location);
                    }

                    activator.Stop(this.BundleContext);
                }
                this.Acitvators = null;
                this.PluginFramework.UnloadDomain(domain);
            }
            catch (Exception ex)
            {
                throw new PluginException(ex.Message, ex);
            }

            this.PluginState = PluginState.Installed;
        }

        public void Uninstall()
        {
            this.PluginFramework.UninstallPlugin(this.SymbolicName);
            this.PluginState = PluginState.Uninstalled;
        }

    }
}
