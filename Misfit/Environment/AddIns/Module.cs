using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Misfit.Environment.AddIns
{
    public class Module
    {
        public string Name { set; get; }
        public ModuleXmlInfo ModuleXmlInfo { set; get; }
        public ActionMapping ActionMapping { set; get; }

        public List<TypeAccessor> TypeAccessors { set; get; }

        public List<AssemblyInfo> ReferenceAssemblyInfos { set; get; }

        public AssemblyInfo RunnerAssembly { set; get; }

        public Module(ModuleXmlInfo moduleXmlInfo)
        {
            this.ModuleXmlInfo = moduleXmlInfo;
        }

        public void Compile()
        {
 
        }

    }
}
