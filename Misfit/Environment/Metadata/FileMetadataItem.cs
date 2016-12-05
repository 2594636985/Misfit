using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Misfit.Environment.Metadata
{
    public class FileMetadataItem : MetadataItem
    {
        public FileMetadataItem()
            : this(null, MetadataType.Normal)
        {

        }

        public FileMetadataItem(MetadataType mType)
            : this(null, mType)
        {

        }
        public FileMetadataItem(IMetadataItem parent, MetadataType mType)
            : base(parent)
        {
            this.MetadataType = mType;
        }

        public override System.IO.FileInfo GetFileInfo()
        {
            string fullname = Path.Combine(this.Location, this.Name);
            if (File.Exists(fullname))
                return new FileInfo(fullname);
            return null;
        }

        public override System.IO.DirectoryInfo GetDirectoryInfo()
        {
            return null;
        }
    }
}
