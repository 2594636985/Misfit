using Misfit.Plugins.Configuration;
using Misfit.Plugins.Logic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Misfit.Plugins.Core
{
    /// <summary>
    /// 对插件程序集的封装
    /// </summary>
    public class PluginAssembly
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

                    Type[] exportedServcieTypes = this.Assembly.GetExportedTypes().Where(t => t.IsClass && (t.IsAssignableFrom(typeof(PluginLogicService)) || t.GetInterfaces().Any(inf => inf.IsAssignableFrom(typeof(IPluginLogicService))))).ToArray();

                    if (exportedServcieTypes != null && exportedServcieTypes.Length > 0)
                    {
                        foreach (Type exportedServcieType in exportedServcieTypes)
                        {
                            ModuleServiceType mst = new ModuleServiceType();
                            mst.ImplementType = exportedServcieType;
                            Type interfaceType = exportedServcieType.GetInterfaces().Where(inf => !inf.IsAssignableFrom(typeof(IPluginLogicService))).FirstOrDefault();
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

        public PluginAssembly(Assembly assembly)
        {
            this.Assembly = assembly;
        }



        public Stream OpenMainifest()
        {
            return this.Assembly.GetManifestResourceStream(string.Format("{0}.Mainifest.xml", this.Assembly.GetName().Name));
        }


    }
}
