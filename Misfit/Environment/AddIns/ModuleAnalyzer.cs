using Misfit.Environment.Metadata;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Misfit.Environment.AddIns
{
    public class ModuleAnalyzer
    {
        private MetadataTree _metadataTree;
        public ModuleAnalyzer(MetadataTree metadataTree)
        {
            this._metadataTree = metadataTree;
        }

        public ReadOnlyCollection<AssemblyInfo> GetPreReferenceAssemblies()
        {
            List<AssemblyInfo> assemblyInfos = new List<AssemblyInfo>();
            List<IMetadataItem> mItems = this._metadataTree.ChildItems.Where(t => t.MetadataType == MetadataType.Dll).ToList();

            foreach (IMetadataItem mItem in mItems)
            {
                if (assemblyInfos.Any(t => t.Name == mItem.Name && t.Version <= mItem.Version))
                {
                    AssemblyInfo assemblyInfo = assemblyInfos.Single(t => t.Name == mItem.Name);

                    if (assemblyInfo.Version < mItem.Version)
                    {
                        assemblyInfos.Remove(assemblyInfo);
                        assemblyInfos.Add(new AssemblyInfo()
                            {
                                Location = mItem.Location,
                                Name = mItem.Name,
                                Version = mItem.Version
                            });
                    }
                }
                else
                {
                    assemblyInfos.Add(new AssemblyInfo()
                    {
                        Location = mItem.Location,
                        Name = mItem.Name,
                        Version = mItem.Version
                    });
                }
            }

            return assemblyInfos.AsReadOnly();
        }

        public ReadOnlyCollection<ModuleXmlInfo> GetModuleXmlInfos()
        {
            List<ModuleXmlInfo> moduleXmlInfos = new List<ModuleXmlInfo>();
            List<IMetadataItem> mItems = this._metadataTree.ChildItems.Where(t => t.MetadataType == MetadataType.Xml).ToList();
            foreach (IMetadataItem mItem in mItems)
            {
                ModuleXmlInfo moduleXmlInfo = new ModuleXmlInfo();
                moduleXmlInfo.Name = mItem.Name;
                moduleXmlInfo.Location = mItem.Location;
                moduleXmlInfo.Version = mItem.Version;

                moduleXmlInfos.Add(moduleXmlInfo);
            }

            return moduleXmlInfos.AsReadOnly();
        }
    }
}
