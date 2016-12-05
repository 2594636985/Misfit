using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Misfit.Environment.Metadata
{
    public class DirMetadataItem : MetadataItem
    {
        public DirMetadataItem()
            : this(null)
        {
        }
        public DirMetadataItem(IMetadataItem parent)
            : base(parent, Metadata.MetadataType.Folder)
        {
        }

        public DirMetadataItem(IMetadataItem parent, MetadataType mType)
            : base(parent, mType)
        {

        }

        public override FileInfo GetFileInfo()
        {
            throw new NotImplementedException();
        }

        public override DirectoryInfo GetDirectoryInfo()
        {
            string fullname = Path.Combine(this.Location, this.Name);
            if (Directory.Exists(fullname))
                return new DirectoryInfo(fullname);
            return null;
        }
    }
}
