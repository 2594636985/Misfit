using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Misfit.Environment.Metadata
{
    public class MetadataTree : DirMetadataItem
    {
        public string RootString { set; get; }
        public string FullLocation { get { return Path.Combine(this.Location, this.RootString); } }
        public MetadataTree()
        {

        }
        public MetadataTree(string rootString, string location)
            : base(null, MetadataType.Root)
        {
            this.RootString = rootString;
            this.Location = location;
        }
    }
}
