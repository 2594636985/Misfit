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
using Misfit.AddIn.Reflection;
using Misfit.AddIn.Services;
using Misfit.AddIn.Utility;

namespace Misfit.AddIn.Core
{
    /// <summary>
    /// 每个Bundle的信息
    /// </summary>
    public class Bundle : IBundle
    {
        private Framework framework;
        private BundleData bundleData;
        protected BundleState state;
        private Int32 id;
        private Assembly assembly;
        private AppDomain domain;
        private IPluginActivator activator;
        private IPluginActivator[] activators;
        private string location;
        private string symbolicName;
        private IPluginContext context;
        private DirectoryInfo storage;

        #region 公共属性
        public BundleState State
        {
            get
            {
                return state;
            }
        }

        public Int32 Id
        {
            get
            {
                return id;
            }
        }

        public string Location
        {
            get
            {
                return location;
            }
        }

        public Framework Framework
        {
            get
            {
                if (framework == null)
                {
                    throw new NullReferenceException("Framework is null.");
                }
                return framework;
            }
        }

        public IPluginContext Context
        {
            get
            {
                if (context == null)
                {
                    context = new BundleContext(this);
                }

                return context;
            }
        }

        private IPluginActivator[] Acitvators
        {
            get
            {
                if (activators == null)
                {
                    AttributeInfo[] attributes = ReflectionUtil.GetCustomAttributes(assembly, typeof(AddInAttribute));
                    if (attributes == null || attributes.Length <= 0)
                    {
                        TracesProvider.TracesOutput.OutputTrace("No activator found");
                        return null;
                    }
                    else
                    {
                        HashSet<IPluginActivator> activatorSet = new HashSet<IPluginActivator>();
                        foreach (AttributeInfo attribute in attributes)
                        {
                            string typeName = attribute.Owner.FullName;
                            object obj = domain.CreateInstanceFromAndUnwrap(location, typeName);
                            IPluginActivator proxy = (IPluginActivator)DynamicProxyFactory.Instance.CreateProxy(obj, new InvocationDelegate(InvocationHandler));
                            activatorSet.Add(proxy);
                        }
                        this.activators = new IPluginActivator[activatorSet.Count];
                        activatorSet.CopyTo(this.activators);
                    }
                }

                return activators;
            }
        }

        internal Assembly Assembly
        {
            get
            {
                return assembly;
            }
            set
            {
                this.assembly = value;
            }
        }

        public Version Version
        {
            get
            {
                return bundleData.Version;
            }
        }
        #endregion

        internal Bundle(BundleData bundleData, Framework framework)
        {
            this.id = bundleData.Id;
            this.storage = new DirectoryInfo(bundleData.Location);
            this.framework = framework;
            this.bundleData = bundleData;
            this.location = bundleData.Location;
            this.symbolicName = Path.GetFileNameWithoutExtension(bundleData.Location);

            this.state = BundleState.Installed;
        }

        private static object InvocationHandler(object target, MethodBase method, object[] parameters)
        {
            Debug.WriteLine("Before: " + method.Name);

            object result = method.Invoke(target, parameters);

            Debug.WriteLine("After: " + method.Name);

            return result;
        }

        public virtual void Start()
        {
            try
            {
                this.state = BundleState.Starting;

                EventManager.OnBundleChanged(new BundleEventArgs(BundleTransition.Starting, this));

                domain = framework.CreateDomain(this.Context);
                Debug.Assert(domain != null, "Bundle AppDomain can't be set to null.");

                AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(AssemblyResolve);

                assembly = Assembly.LoadFrom(location);
                Debug.Assert(assembly != null, "Bundle Assembly can's be set to null.");

                foreach (IPluginActivator activator in this.Acitvators)
                {
                    if (activator == null)
                    {
                        throw new BundleException("No activator for: " + this.Location);
                    }

                    activator.Start(this.Context);
                }

                this.state = BundleState.Active;

                EventManager.OnBundleChanged(new BundleEventArgs(BundleTransition.Started, this));
            }
            catch (Exception ex)
            {
                this.state = BundleState.Installed;
                throw new BundleException(ex.Message, ex);
            }
        }

        private void LoadAssembly(string assemblyName)
        {
            string assemblyFile = SearchAssembly(assemblyName);
            Assembly.LoadFrom(assemblyFile);
        }

        private static string SearchAssembly(string assemblyName)
        {
            string _addInsRoot = PropertyHelper.GetData<string>("Misfit.AddInsRoot");
            string appRoot = AppDomain.CurrentDomain.BaseDirectory;
            string addInsRoot = Path.Combine(appRoot, _addInsRoot);

            // Search root dir
            {
                string[] files = Directory.GetFiles(appRoot,
                    assemblyName + ".dll", SearchOption.TopDirectoryOnly);

                if (files != null && files.Length > 0)
                {
                    return files[0];
                }
            }

            // Search AddIns dir
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
                this.state = BundleState.Stopping;
                EventManager.OnBundleChanged(new BundleEventArgs(BundleTransition.Stopping, this));

                foreach (IPluginActivator activator in this.Acitvators)
                {
                    if (activator == null)
                    {
                        throw new Exception("No activator for: " + this.Location);
                    }

                    activator.Stop(this.Context);
                }
                activators = null;
                framework.UnloadDomain(domain);
            }
            catch (Exception ex)
            {
                throw new BundleException(ex.Message, ex);
            }

            this.state = BundleState.Installed;
            EventManager.OnBundleChanged(new BundleEventArgs(BundleTransition.Stopped, this));
        }

        public void Uninstall()
        {
            framework.UninstallBundle(id);
            this.state = BundleState.Uninstalled;
            EventManager.OnBundleChanged(new BundleEventArgs(BundleTransition.Uninstalled, this));
        }

        #region IBundle Members

        public void Update()
        {
            throw new NotImplementedException();
        }

        public void Update(Stream inputStream)
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, object> GetProperties()
        {
            throw new NotImplementedException();
        }

        public IServiceReference[] GetRegisteredServices()
        {
            throw new NotImplementedException();
        }

        public IServiceReference[] GetServicesInUse()
        {
            throw new NotImplementedException();
        }

        public Uri GetResource(string name)
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, object> GetProperties(string locale)
        {
            throw new NotImplementedException();
        }

        public string SymbolicName
        {
            get { return symbolicName; }
        }

        public Type LoadClass(string name)
        {
            throw new NotImplementedException();
        }

        public IEnumerator GetResources(string name)
        {
            throw new NotImplementedException();
        }

        public IEnumerator GetEntryPaths(string name)
        {
            throw new NotImplementedException();
        }

        public IEnumerator GetEntry(string name)
        {
            throw new NotImplementedException();
        }

        public long GetLastModified()
        {
            throw new NotImplementedException();
        }

        public IEnumerator FindEntries(string path, string filePattern, bool recurse)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
