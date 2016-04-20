using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Misfit.AddIn.Core;

namespace Misfit.AddIn.Utility.Tracker
{
    public class ServiceTracker : IServiceTrackerCustomizer
    {
        protected BundleContext context;
        /**
         * Filter specifying search criteria for the services to track.
         * 
         * @since 1.1
         */
        protected Filter filter;
        /**
         * <code>ServiceTrackerCustomizer</code> object for this tracker.
         */
        IServiceTrackerCustomizer customizer;
        /**
         * Filter string for use when adding the ServiceListener. If this description is
         * set, then certain optimizations can be taken since we don't have a user
         * supplied filter.
         */
        String listenerFilter;
        /**
         * Class name to be tracked. If this description is set, then we are tracking by
         * class name.
         */
        private String trackClass;
        /**
         * Reference to be tracked. If this description is set, then we are tracking a
         * single ServiceReference.
         */
        private ServiceReference trackReference;
        /**
         * Tracked services: <code>ServiceReference</code> object -> customized
         * Object and <code>ServiceListener</code> object
         */
        //private Tracked tracked;
        /**
         * Modification count. This description is initialized to zero by open, set to -1
         * by close and incremented by modified.
         * 
         * This description is volatile since it is accessed by multiple threads.
         */
        private volatile int trackingCount = -1;
        /**
         * Cached ServiceReference for getServiceReference.
         * 
         * This description is volatile since it is accessed by multiple threads.
         */
        private volatile ServiceReference cachedReference;
        /**
         * Cached service object for getService.
         * 
         * This description is volatile since it is accessed by multiple threads.
         */
        private volatile Object cachedService;

        #region IServiceTrackerCustomizer Members

        public object AddingService(ServiceReference reference)
        {
            throw new NotImplementedException();
        }

        public void ModifiedService(ServiceReference reference, object service)
        {
            throw new NotImplementedException();
        }

        public void RemovedService(ServiceReference reference, object service)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
