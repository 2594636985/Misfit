using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Misfit.AddIn.Core
{
    public class BundleData
    {
        private int id;
        private string location;
        private Version version;

        public int Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }

        public string Location
        {
            get
            {
                return location;
            }
            set
            {
                location = value;
            }
        }

        public Version Version
        {
            get
            {
                return version;
            }
            set
            {
                version = value;
            }
        }

        public BundleData()
        {

        }

        public BundleData(int id, string location)
        {
            this.id = id;
            this.location = location;
        }
    }
}
