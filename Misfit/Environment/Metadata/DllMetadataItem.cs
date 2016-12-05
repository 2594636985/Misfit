using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Misfit.Environment.Metadata
{
    public class DllMetadataItem : FileMetadataItem
    {
        public DllMetadataItem()
            : base(null, MetadataType.Dll)
        {

        }
        public DllMetadataItem(IMetadataItem parent)
            : base(parent, MetadataType.Dll)
        {

        }

    }
}
