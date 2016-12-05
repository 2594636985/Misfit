using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace Misfit.Environment.Metadata
{
    public class MetadataTreeBuilder
    {
        private MetadataTree _mTree;
        public MetadataTreeBuilder(MetadataTree mTree)
        {
            this._mTree = mTree;
        }
        public static MetadataTreeBuilder Initialize(string location, string rootString)
        {
            MetadataTreeBuilder builder = new MetadataTreeBuilder(new MetadataTree(location, rootString));
            return builder;
        }

        public MetadataTree Analyze()
        {
            if (string.IsNullOrWhiteSpace(this._mTree.RootString))
                throw new NullReferenceException("元数据树的根节点(RootString)不能为空");
            if (string.IsNullOrWhiteSpace(this._mTree.Location))
                throw new NullReferenceException("元数据树的所在的位置(Location)不能为空");

            if (!Directory.Exists(this._mTree.FullLocation))
                Directory.CreateDirectory(this._mTree.FullLocation);

            DirectoryInfo diLocation = new DirectoryInfo(this._mTree.FullLocation);

            DirectoryInfo[] directoryInfoArr = diLocation.GetDirectories();
            foreach (DirectoryInfo directoryInfo in directoryInfoArr)
            {
                IMetadataItem dMetadataItem = this.ConvertToMetadataItem(directoryInfo);
                this._mTree.ChildItems.Add(dMetadataItem);
            }

            return this._mTree;
        }


        private IMetadataItem ConvertToMetadataItem(DirectoryInfo dInfo)
        {
            IMetadataItem dirMetadataItem = new DirMetadataItem(this._mTree);
            FileInfo[] fileInfoArr = dInfo.GetFiles();
            if (fileInfoArr != null && fileInfoArr.Length > 0)
            {
                foreach (FileInfo fileInfo in fileInfoArr)
                {
                    IMetadataItem mTtem = this.ConvertToMetadataItem(fileInfo);
                    mTtem.Parent = dirMetadataItem;
                    dirMetadataItem.ChildItems.Add(mTtem);
                }
            }

            DirectoryInfo[] directoryInofArr = dInfo.GetDirectories();

            if (directoryInofArr != null && directoryInofArr.Length > 0)
            {
                foreach (DirectoryInfo directoryInfo in directoryInofArr)
                {
                    dirMetadataItem.ChildItems.Add(ConvertToMetadataItem(directoryInfo));
                }
            }
            return dirMetadataItem;
        }

        private IMetadataItem ConvertToMetadataItem(FileInfo fileInfo)
        {
            IMetadataItem mTtem;
            if (string.Compare(fileInfo.Extension, "dll", true) == 0)
            {
                mTtem = new DllMetadataItem();
            }
            else if (string.Compare(fileInfo.Extension, "xml", true) == 0)
            {
                mTtem = new XmlMetadataItem();
            }
            else
                mTtem = new FileMetadataItem();

            mTtem.Location = fileInfo.DirectoryName;
            mTtem.Name = fileInfo.Name;
            mTtem.Version = new Version(FileVersionInfo.GetVersionInfo(fileInfo.FullName).FileVersion);

            return mTtem;
        }




    }
}
