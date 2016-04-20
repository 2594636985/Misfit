using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Misfit.AddIn;
using System.Threading;

namespace Misfit.Core
{
    /// <summary>
    /// 储存Bundle的库存
    /// </summary>
    public class BundleRepository : IBundleRepository
    {
        private List<IPlugin> _bundlesByInstallOrder;

        private Dictionary<int, IPlugin> _bundlesById;

        private Dictionary<string, IPlugin> _bundlesBySymbolicName;

        private int _nextBundleID;

        private object syncObj;

        public int Count
        {
            get
            {
                lock (this)
                {
                    return _bundlesByInstallOrder.Count;
                }
            }
        }

        public BundleRepository()
        {
            this._bundlesByInstallOrder = new List<IPlugin>();
            this._bundlesById = new Dictionary<int, IPlugin>();
            this._bundlesBySymbolicName = new Dictionary<string, IPlugin>();

            this.syncObj = new object();
        }

        public IPlugin this[int index]
        {
            get
            {
                lock (syncObj)
                {
                    return _bundlesByInstallOrder[index];
                }
            }
        }

        public IPlugin[] GetBundles()
        {
            lock (syncObj)
            {
                return _bundlesByInstallOrder.ToArray<IPlugin>();
            }
        }

        /// <summary>
        /// 根据唯一标识ID来获得Bundle
        /// </summary>
        /// <param name="bundleId"></param>
        /// <returns></returns>
        public IPlugin GetBundle(int bundleId)
        {
            lock (syncObj)
            {
                return (Plugin)_bundlesById[bundleId];
            }
        }

        /// <summary>
        /// 根据标识名称和版本号来获得Bundle
        /// </summary>
        /// <param name="symbolicName"></param>
        /// <returns></returns>
        public IPlugin[] GetBundles(string symbolicName)
        {
            lock (syncObj)
            {
                List<IPlugin> bundles = new List<IPlugin>();
                Dictionary<string, IPlugin>.Enumerator enumerator = _bundlesBySymbolicName.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    if (string.Compare(enumerator.Current.Key, symbolicName, true) == 0)
                    {
                        bundles.Add(enumerator.Current.Value);
                    }
                }

                return bundles.ToArray<IPlugin>();
            }
        }

        /// <summary>
        /// 根据标识名称和版本号来获得Bundle
        /// </summary>
        /// <param name="symbolicName"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        public IPlugin GetBundle(string symbolicName, Version version)
        {
            IPlugin[] bundles = GetBundles(symbolicName);
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
        public void Register(IPlugin bundle)
        {
            lock (syncObj)
            {
                Interlocked.Increment(ref this._nextBundleID);
                bundle.ModuleID = this._nextBundleID;
                _bundlesByInstallOrder.Add(bundle);
                _bundlesById.Add(bundle.ModuleID, bundle);
                _bundlesBySymbolicName.Add(bundle.SymbolicName, bundle);
            }
        }

        /// <summary>
        /// 注辙一个Bundle
        /// </summary>
        /// <param name="bundle"></param>
        /// <returns></returns>
        public bool Unregister(IPlugin bundle)
        {
            lock (syncObj)
            {
                bool found = _bundlesByInstallOrder.Remove(bundle);
                if (!found)
                {
                    return false;
                }

                _bundlesById.Remove(bundle.ModuleID);
                _bundlesBySymbolicName.Remove(bundle.SymbolicName);
                return true;
            }
        }

        /// <summary>
        /// 获得下个BUNDLE的ID
        /// </summary>
        /// <returns></returns>
        public int GetNextBundleId()
        {
            Interlocked.Increment(ref this._nextBundleID);

            return this._nextBundleID;
        }
    }
}
