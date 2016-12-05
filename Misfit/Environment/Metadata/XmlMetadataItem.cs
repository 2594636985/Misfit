using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Misfit.Environment.Metadata
{
    public class XmlMetadataItem:FileMetadataItem
    {
        public XmlMetadataItem()
            : base(null, MetadataType.Xml)
        { 
        }
        public XmlMetadataItem(IMetadataItem parent)
            : base(parent, MetadataType.Xml)
        { 
        }
    }
}
