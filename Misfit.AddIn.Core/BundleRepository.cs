using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Misfit.AddIn;

namespace Misfit.AddIn.Core
{
    /// <summary>
    /// 储存Bundle的库存
    /// </summary>
    public class BundleRepository : IBundleRepository
    {
        private List<IBundle> bundlesByInstallOrder;

        private Dictionary<int, IBundle> bundlesById;

        private Dictionary<string, IBundle> bundlesBySymbolicName;

        private object syncObj;

        public int Count
        {
            get
            {
                lock (this)
                {
                    return bundlesByInstallOrder.Count;
                }
            }
        }

        public BundleRepository()
        {
            this.bundlesByInstallOrder = new List<IBundle>();
            this.bundlesById = new Dictionary<int, IBundle>();
            this.bundlesBySymbolicName = new Dictionary<string, IBundle>();

            this.syncObj = new object();
        }

        public IBundle this[int index]
        {
            get
            {
                lock (syncObj)
                {
                    return bundlesByInstallOrder[index];
                }
            }
        }

        public IBundle[] GetBundles()
        {
            lock (syncObj)
            {
                return bundlesByInstallOrder.ToArray<IBundle>();
            }
        }

        /// <summary>
        /// 根据唯一标识ID来获得Bundle
        /// </summary>
        /// <param name="bundleId"></param>
        /// <returns></returns>
        public IBundle GetBundle(int bundleId)
        {
            lock (syncObj)
            {
                return (Bundle)bundlesById[bundleId];
            }
        }

        /// <summary>
        /// 根据标识名称和版本号来获得Bundle
        /// </summary>
        /// <param name="symbolicName"></param>
        /// <returns></returns>
        public IBundle[] GetBundles(string symbolicName)
        {
            lock (syncObj)
            {
                List<IBundle> bundles = new List<IBundle>();
                Dictionary<string, IBundle>.Enumerator enumerator = bundlesBySymbolicName.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    if (string.Compare(enumerator.Current.Key, symbolicName, true) == 0)
                    {
                        bundles.Add(enumerator.Current.Value);
                    }
                }

                return bundles.ToArray<IBundle>();
            }
        }

        /// <summary>
        /// 根据标识名称和版本号来获得Bundle
        /// </summary>
        /// <param name="symbolicName"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        public IBundle GetBundle(string symbolicName, Version version)
        {
            IBundle[] bundles = GetBundles(symbolicName);
            if (bundles != null)
            {
                if (bundles.Length > 0)
                {
                    for (int i = 0; i < bundles.Length; i++)
                    {
                        return bundles[i];
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// 注册一个Bundle
        /// </summary>
        /// <param name="bundle"></param>
        public void Register(IBundle bundle)
        {
            lock (syncObj)
            {
                bundlesByInstallOrder.Add(bundle);
                bundlesById.Add(bundle.Id, bundle);
                bundlesBySymbolicName.Add(bundle.SymbolicName, bundle);
            }
        }

        /// <summary>
        /// 注辙一个Bundle
        /// </summary>
        /// <param name="bundle"></param>
        /// <returns></returns>
        public bool Unregister(IBundle bundle)
        {
            lock (syncObj)
            {
                bool found = bundlesByInstallOrder.Remove(bundle);
                if (!found)
                {
                    return false;
                }

                bundlesById.Remove(bundle.Id);
                bundlesBySymbolicName.Remove(bundle.SymbolicName);
                return true;
            }
        }
    }
}
