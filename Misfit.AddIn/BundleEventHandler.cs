using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Misfit.AddIn
{
    /// <summary>
    /// Prototype of the method to be implemented to receive bundle events.
    /// </summary>
    /// <param name="sender">The event sender</param>
    /// <param name="e">The event argurment <see cref="BundleEventArgs"/></param>
    public delegate void BundleEventHandler(object sender, BundleEventArgs e);
}
