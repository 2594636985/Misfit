using Misfit.Modulation.AddIn.Tracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Misfit.Modulation.Tracking
{
    public class Tracker : MarshalByRefObject, ITracker
    {
        public ITarget Target { set; get; }
        public string DomainName { set; get; }
        public Tracker(string domainName)
        {
            this.DomainName = domainName;
        }

        public Tracker(string domainName, ITarget target)
        {
            this.DomainName = domainName;
            this.Target = target;
        }

        public void Info(string message)
        {
            this.Target.WriteLine(message, TrackerLevel.Info);
        }

        public void Error(string message)
        {

        }
    }
}
