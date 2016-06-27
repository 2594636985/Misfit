using Misfit.Modulation.AddIn.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Misfit.Modulation.AddIn.IO
{
    public class AddInConfiguration
    {
        public ModuleAssembly ModuleAssembly { private set; get; }

        public Mainifest MainifestDescriptor { private set; get; }

        public AddInConfiguration(ModuleAssembly moduleAssembly)
        {
            this.ModuleAssembly = moduleAssembly;
        }
        public void Initialize()
        {
            Stream fileStream = this.ModuleAssembly.OpenMainifest();
            if (fileStream == null)
                throw new NullReferenceException("没有找到服务配置文件Mainifest.xml 或是不存程序集的根目录下");

            MainifestDocument mainifestDocument = new MainifestDocument();
            mainifestDocument.Load(fileStream);
            fileStream.Close();

            MainifestNode mainifestNode = mainifestDocument.MainifestNode;
            if (mainifestNode != null)
            {
                this.MainifestDescriptor = new Mainifest();
                this.MainifestDescriptor.Name = mainifestNode.Name;
                this.MainifestDescriptor.ConnectString = mainifestNode.ConnectString;

                foreach (ServiceNode beanNode in mainifestNode.ServiceNodes)
                {
                    ServiceDescriptor serviceDescriptor = new ServiceDescriptor();

                    serviceDescriptor.Name = beanNode.Name;
                    serviceDescriptor.ClassName = beanNode.ClassName;

                    this.MainifestDescriptor.ServiceDescriptors.Add(serviceDescriptor);
                }

            }
        }
    }
}
