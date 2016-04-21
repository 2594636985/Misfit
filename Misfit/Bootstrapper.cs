using Misfit.AddIn;
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
        private string _pluginConfigFilename;
        private PluginFramework _pluginFramework;
        public event Action<Bootstrapper> OnExited;

        public Bootstrapper(string filename = "Plugins.xml")
        {
            if (!Path.IsPathRooted(filename))
                this._pluginConfigFilename = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, filename);
            else
                this._pluginConfigFilename = filename;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public void Initialize()
        {
            if (!File.Exists(this._pluginConfigFilename))
                throw new FileNotFoundException(string.Format("没有找到对应的配置文件 {0}", this._pluginConfigFilename));

            this._pluginFramework = new PluginFramework();
            this._pluginFramework.OnModuleInstalled += ModuleRuntime_OnModuleInstalled;

            PluginsDocument pDoc = new PluginsDocument();
            pDoc.Load(this._pluginConfigFilename);

            List<Plugin> bundles = pDoc.ChildNodes.ToBundleList().OrderBy(t => t.Level).ToList();

            if (bundles != null && bundles.Count > 0)
            {
                foreach (Plugin bundle in bundles)
                {
                    this._pluginFramework.InstallPlugin(bundle);
                }
            }

            this._pluginFramework.Start();
        }


        public void Execute()
        {

        }

        private void ModuleRuntime_OnModuleInstalled(PluginFramework pluginFramework, IPlugin plugin)
        {

        }


    }
}
