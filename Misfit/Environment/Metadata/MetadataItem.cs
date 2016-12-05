using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Misfit.Environment.Metadata
{
    public abstract class MetadataItem : IMetadataItem
    {
        public MetadataItem()
            : this(null)
        {

        }
        public MetadataItem(IMetadataItem parent)
            : this(parent, MetadataType.Normal)
        {

        }
        public MetadataItem(IMetadataItem parent, MetadataType mType)
        {
            this.Parent = parent;
            this.MetadataType = mType;
            this.ChildItems = new List<IMetadataItem>();
        }
        public string Name { set; get; }
        public string Location { set; get; }

        public Version Version { set; get; }
        public string IMetadataItem.FullLocation
        {
            get
            {
                return Path.Combine(this.Location, this.Name);
            }
        }

        public abstract FileInfo GetFileInfo();

        public abstract DirectoryInfo GetDirectoryInfo();

        public MetadataType MetadataType { set; get; }

        public IMetadataItem Parent { set; get; }

        public List<IMetadataItem> ChildItems { set; get; }

    }
}
