using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Misfit.Environment.Metadata
{
    public interface IMetadataItem
    {
        string Name { set; get; }

        MetadataType MetadataType { set; get; }

        IMetadataItem Parent { set; get; }

        List<IMetadataItem> ChildItems { set; get; }

        string Location { set; get; }
        Version Version { set; get; }

        string FullLocation { get; }

        FileInfo GetFileInfo();

        DirectoryInfo GetDirectoryInfo();

    }
}
