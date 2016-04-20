using Misfit.Core;
using Misfit.Xml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Misfit
{
    public class Bootstrapper
    {
        private string _pluginsConfigurationFilename;
        private PluginFramework _moduleRuntimeLibrary;
        public event Action<Bootstrapper> OnExited;

        public Bootstrapper(string filename = "Plugins.xml")
        {
            if (!Path.IsPathRooted(filename))
                this._pluginsConfigurationFilename = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, filename);
            else
                this._pluginsConfigurationFilename = filename;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public void Initialize()
        {
            if (!File.Exists(this._pluginsConfigurationFilename))
                throw new FileNotFoundException(string.Format("没有找到对应的配置文件 {0}", this._pluginsConfigurationFilename));

            this._moduleRuntimeLibrary = new PluginFramework();
            this._moduleRuntimeLibrary.OnModuleInstalled += ModuleRuntime_OnModuleInstalled;
            this._moduleRuntimeLibrary.Initialize();

            PluginsDocument pDoc = new PluginsDocument();
            pDoc.Load(this._pluginsConfigurationFilename);

            List<Plugin> bundles = pDoc.ChildNodes.ToBundleList().OrderBy(t => t.Level).ToList();

            if (bundles != null && bundles.Count > 0)
            {
                foreach (Plugin bundle in bundles)
                {
                    this._moduleRuntimeLibrary.InstallPlugin(bundle);
                }
            }
        }


        public void Execute()
        {
 
        }

        private void ModuleRuntime_OnModuleInstalled(PluginFramework arg1, IModule arg2)
        {

        }


    }
}
