using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Misfit.AddIn
{
    public class BundleEventArgs : EventArgs
    {
        private IBundle bundle;
        private BundleTransition transition;

        public IBundle Bundle
        {
            get { return bundle; }
        }

        public BundleTransition Transition
        {
            get { return transition; }
        }

        public BundleEventArgs(BundleTransition transition, IBundle bundle)
        {
            this.bundle = bundle;
            this.transition = transition;
        }
    }
}
