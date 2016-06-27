using Misfit.Modulation.AddIn.IO;
using Misfit.Modulation.AddIn.Serices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Misfit.Modulation.AddIn.Core
{
    /// <summary>
    /// 对模块程序集的封装
    /// </summary>
    public class ModuleAssembly
    {
        private Mainifest _mainifest;
        private List<ModuleServiceType> _moduleServiceTypes;

        public Assembly Assembly { private set; get; }

        public List<ModuleServiceType> ModuleServiceTypes
        {
            get
            {
                if (this._moduleServiceTypes == null)
                {
                    this._moduleServiceTypes = new List<ModuleServiceType>();

                    Type[] exportedServcieTypes = this.Assembly.GetExportedTypes().Where(t => t.IsClass && (t.IsAssignableFrom(typeof(MisfitAbsService)) || t.GetInterfaces().Any(inf => inf.IsAssignableFrom(typeof(IMisfitService))))).ToArray();

                    if (exportedServcieTypes != null && exportedServcieTypes.Length > 0)
                    {
                        foreach (Type exportedServcieType in exportedServcieTypes)
                        {
                            ModuleServiceType mst = new ModuleServiceType();
                            mst.ImplementType = exportedServcieType;
                            Type interfaceType = exportedServcieType.GetInterfaces().Where(inf => !inf.IsAssignableFrom(typeof(IMisfitService))).FirstOrDefault();
                            if (interfaceType != null)
                                mst.InterfaceType = interfaceType;

                            this._moduleServiceTypes.Add(mst);
                        }
                    }
                }

                return this._moduleServiceTypes;
            }
        }

        public Version Version
        {
            get
            {
                return this.Assembly.GetName().Version;
            }
        }

        public Mainifest Mainifest
        {
            get
            {
                if (this._mainifest == null)
                {
                    Stream fileStream = this.OpenMainifest();
                    if (fileStream == null)
                        throw new NullReferenceException("没有找到服务配置文件Mainifest.xml 或是不存程序集的根目录下");

                    MainifestDocument mainifestDocument = new MainifestDocument();
                    mainifestDocument.Load(fileStream);
                    fileStream.Close();

                    MainifestNode mainifestNode = mainifestDocument.MainifestNode;

                    if (mainifestNode != null)
                    {
                        this._mainifest = new Mainifest();
                        this._mainifest.Name = mainifestNode.Name;
                        this._mainifest.ConnectString = mainifestNode.ConnectString;

                        foreach (ServiceNode beanNode in mainifestNode.ServiceNodes)
                        {
                            ServiceDescriptor serviceDescriptor = new ServiceDescriptor();

                            serviceDescriptor.Name = beanNode.Name;
                            serviceDescriptor.ClassName = beanNode.ClassName;

                            this._mainifest.ServiceDescriptors.Add(serviceDescriptor);
                        }

                    }
                }
              
                return _mainifest;
            }
        }

        public ModuleAssembly(Assembly assembly)
        {
            this.Assembly = assembly;
        }



        public Stream OpenMainifest()
        {
            return this.Assembly.GetManifestResourceStream(string.Format("{0}.Mainifest.xml", this.Assembly.GetName().Name));
        }


    }
}
