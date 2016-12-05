using Misfit.Environment.AddIns;
using Misfit.Environment.Metadata;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Misfit.Environment
{
    public class Dispatcher
    {

        public void Initialize()
        {
            AssemblyLocator.Initialize();

            MetadataTree mTree = MetadataTreeBuilder.Initialize("", "").Analyze();

            ModuleAnalyzer moduleAnalyzer = new ModuleAnalyzer(mTree);

            ReadOnlyCollection<AssemblyInfo> preReferenceAssemblies = moduleAnalyzer.GetPreReferenceAssemblies();
            ReadOnlyCollection<ModuleXmlInfo> moduleXmlInfos = moduleAnalyzer.GetModuleXmlInfos();

            foreach (AssemblyInfo assemblyInfo in preReferenceAssemblies)
            {
                AssemblyLocator.AddAssebmlyInfo(assemblyInfo);
            }

            foreach (ModuleXmlInfo moduleXmlInfo in moduleXmlInfos)
            {
                Module modeule = new Module(moduleXmlInfo);
                modeule.Compile();
            }

        }

        
    }
}
