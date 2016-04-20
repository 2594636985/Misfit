using System;
using System.Collections.Generic;

namespace Misfit.AddIn
{
    public interface IBundleRepository
    {
        void Register(IBundle bundle);
        bool Unregister(IBundle bundle);
        int Count { get; }
        IBundle GetBundle(int bundleId);
        IBundle GetBundle(string symbolicName, Version version);
        IBundle[] GetBundles(string symbolicName);
        IBundle[] GetBundles();
        IBundle this[int index] { get; }

        /// <summary>
        /// 获得下个BUNDLE的ID
        /// </summary>
        /// <returns></returns>
        int GetNextBundleId();
    }
}
