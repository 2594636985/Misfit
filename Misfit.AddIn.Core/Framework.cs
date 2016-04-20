using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Security;
using System.Text.RegularExpressions;
using System.Threading;
using Misfit.AddIn;
using Misfit.AddIn.Services;
using Misfit.AddIn.Utility;

namespace Misfit.AddIn.Core
{
    public class Framework : IFramework
    {
        private const string BundleExtention = ".dll";
        private BundleRepository bundleRepository;
        private ServiceRegistry serviceRegistry;
        private SystemBundle systemBundle;
        private BundleContext systemBundleContext;
        private int bundleAppDomains;
        private int serviceId;

        public IBundleRepository Bundles
        {
            get
            {
                return bundleRepository;
            }
        }

        internal ServiceRegistry ServiceRegistry
        {
            get
            {
                return serviceRegistry;
            }
        }

        public event FrameworkEventHandler FrameworkEvent
        {
            add
            {
                EventManager.FrameworkEvent += value;
            }
            remove
            {
                EventManager.FrameworkEvent -= value;
            }
        }

        public void InitializeFramework()
        {
            serviceId = 1;
            if (bundleRepository == null)
            {
                bundleRepository = new BundleRepository();
            }
            else
            {
                throw new InvalidOperationException("BundleÈÝÆ÷ÒÑ¾­´æÔÚ£¡");
            }

            if (serviceRegistry == null)
            {
                serviceRegistry = new ServiceRegistry();
            }
            else
            {
                throw new InvalidOperationException("The framework is already started");
            }

            systemBundle = new SystemBundle(this);
            systemBundleContext = (BundleContext)systemBundle.Context;
        }

        public void Launch()
        {
            InstallBundleInternal(systemBundle);
            StartBundle(systemBundle);

            // Launch auto start AddIns
            string addInsSet = PropertyHelper.GetData<string>("Misfit.AddIns");
            string pattern = @"(?<AddIn>[\w.]*)@(?<Id>[\d]{1,5})[:](?<Action>start|install)";
            MatchCollection matchs = Regex.Matches(addInsSet, pattern, RegexOptions.IgnorePatternWhitespace);
            foreach (Match match in matchs)
            {
                string addIn = match.Groups["AddIn"].Value;
                string id = match.Groups["Id"].Value;
                string action = match.Groups["Action"].Value;

                IBundle bundle = null;
                if (action == "install" ||
                    action == "start")
                {
                    bundle = InstallBundle(addIn);
                    Debug.Assert(bundle != null);
                }

                if (action == "start")
                {
                    StartBundle(bundle);
                }
            }
        }

        public void Close()
        {
            Debug.Assert(bundleRepository != null);

            for (Int32 i = bundleRepository.Count - 1; i > -1; i--)
            {
                bundleRepository[i].Stop();
            }
        }

        public void Shutdown()
        {
            Debug.Assert(bundleRepository != null);

            for (int i = bundleRepository.Count - 1; i >= 0; i--)
            {
                IBundle bundle = bundleRepository[i];
                int bundleId = bundle.Id;
                StopBundle(bundleId);
                UninstallBundle(bundleId);
                bundle = null;
            }
        }

        public IBundle InstallBundle(string location)
        {
            if (location.IndexOf(BundleExtention,
                StringComparison.OrdinalIgnoreCase) == -1)
            {
                location = location + BundleExtention;
            }

            string fullLocation = string.Empty;
            string _addInsRoot = PropertyHelper.GetData<string>("Misfit.AddInsRoot");
            string appRoot = Path.GetDirectoryName(typeof(Framework).Assembly.Location);
            string addInsRoot = Path.Combine(appRoot, _addInsRoot);

            Debug.Assert(!string.IsNullOrEmpty(addInsRoot));

            bool isPathRooted = Path.IsPathRooted(location);

            //Debug.WriteLine("IsPathRooted:" + isPathRooted);

            if (isPathRooted)
            {
                fullLocation = location;
            }
            else
            {
                fullLocation = Path.Combine(addInsRoot, location);
            }

            if (!File.Exists(fullLocation))
            {
                throw new BundleException(String.Format("Bundle {0} not found.", location),
                    new FileNotFoundException(String.Format("file:{0} not found.", fullLocation)));
            }

            // Create the bundle object
            BundleData bundleData = new BundleData();
            bundleData.Id = bundleRepository.Count;
            bundleData.Location = fullLocation;

            Bundle bundle = new Bundle(bundleData, this);

            CheckInstallBundle(bundle);

            InstallBundleInternal(bundle);

            return bundle;
        }

        private void CheckInstallBundle(Bundle bundle)
        {
            IBundle existsBundle = bundleRepository.GetBundle(bundle.SymbolicName, null);
            if (existsBundle != null)
            {
                throw new BundleException("Bundle is already installed.");
            }
        }

        private IBundle InstallBundleInternal(Bundle bundle)
        {
            bundleRepository.Register(bundle);

            if (bundle.State == BundleState.Installed)
            {
                EventManager.OnBundleChanged(
                    new BundleEventArgs(
                        BundleTransition.Installed, bundle)
                        );
            }

            return bundle;
        }

        public void UninstallBundle(int id)
        {
            try
            {
                IBundle bundle = bundleRepository.GetBundle(id);
                bundleRepository.Unregister(bundle);
            }
            catch (Exception ex)
            {
                throw new BundleException("Uninstall bundle threw a exception.", ex);
            }
        }

        public IBundle StartBundle(int id)
        {
            IBundle bundle = bundleRepository.GetBundle(id);
            if (bundle == null)
            {
                throw new BundleException(String.Format("Bundle not found.BundleId:{0}", id));
            }
            if (bundle.State != BundleState.Installed)
            {
                throw new BundleException("Bundle is aready started.");
            }
            bundle.Start();

            return bundle;
        }

        public void StopBundle(int id)
        {
            IBundle bundle = bundleRepository.GetBundle(id);
            if (bundle == null)
            {
                throw new BundleException(
                    String.Format("Bundle not found.BundleId:{0}", id));
            }
            if (bundle.State != BundleState.Active)
            {
                throw new BundleException("Bundle is not active.");
            }
            bundle.Stop();
        }

        public void StartBundle(IBundle bundle)
        {
            if (bundle == null)
            {
                throw new ArgumentNullException("bundle");
            }
            bundle.Start();
        }

        public IServiceReference[] GetServiceReferences(string clazz, string filterString,
            IPluginContext context, bool allservices)
        {
            Filter filter = string.IsNullOrEmpty(filterString) ? null : new Filter(filterString);
            IServiceReference[] services = null;

            lock (serviceRegistry)
            {
                services = serviceRegistry.LookupServiceReferences(clazz, filter);
                if (services == null)
                {
                    return null;
                }
                int removed = 0;
                for (int i = services.Length - 1; i >= 0; i--)
                {
                    ServiceReference reference = (ServiceReference)services[i];
                    string[] classes = reference.GetClasses();
                    if (allservices || context.IsAssignableTo((ServiceReference)services[i]))
                    {
                        if (clazz == null)
                            try
                            { /* test for permission to the classes */
                                //checkGetServicePermission(classes);
                            }
                            catch (SecurityException ex)
                            {
                                services[i] = null;
                                removed++;
                            }
                    }
                    else
                    {
                        services[i] = null;
                        removed++;
                    }
                }
                if (removed > 0)
                {
                    IServiceReference[] temp = services;
                    services = new ServiceReference[temp.Length - removed];
                    for (int i = temp.Length - 1; i >= 0; i--)
                    {
                        if (temp[i] == null)
                            removed--;
                        else
                            services[i - removed] = temp[i];
                    }
                }

            }
            return services == null || services.Length == 0 ? null : services;
        }

        public int GetNextServiceId()
        {
            int serviceId = this.serviceId;
            serviceId++;
            return serviceId;
        }

        public AppDomain CreateDomain(IPluginContext context)
        {
            AppDomainSetup info = new AppDomainSetup();
            info.ApplicationBase = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "AddIns");
            info.ShadowCopyDirectories = Path.Combine(info.ApplicationBase, @"cache");
            info.ShadowCopyFiles = "true";
            string domainName = "Bundle-" + context.Bundle.Id.ToString().PadLeft(3, '0');
            AppDomain domain = AppDomain.CreateDomain(domainName, AppDomain.CurrentDomain.Evidence, info);

            Interlocked.Increment(ref this.bundleAppDomains);

            return domain;
        }

        public void UnloadDomain(AppDomain domain)
        {
            if (domain != null)
            {
                AppDomain.Unload(domain);
                Interlocked.Decrement(ref this.bundleAppDomains);
            }
        }
    }
}
