using Misfit.AddIn;
using Misfit.Console;
using Misfit.Core;
using Misfit.Xml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Misfit
{
    public class Bootstrapper
    {
        private ConsoleService _logConsoleService;
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
            this._pluginFramework.OnPluginInstalled += PluginFramework_OnPluginInstalled;

            PluginsDocument pDoc = new PluginsDocument();
            pDoc.Load(this._pluginConfigFilename);

            //开启调试状态
            if (pDoc.ChildNodes.Debug)
            {
                if (this._logConsoleService == null)
                    this._logConsoleService = new ConsoleService();

                this._logConsoleService.Start();
            }

            List<Plugin> plugins = pDoc.ChildNodes.ToPluginList().OrderBy(t => t.Level).ToList();

            if (plugins != null && plugins.Count > 0)
            {
                foreach (Plugin plugin in plugins)
                {
                    this._pluginFramework.InstallPlugin(plugin);
                }
            }

        }

        /// <summary>
        /// 安装新的插件时，发生
        /// </summary>
        /// <param name="pluginFramework"></param>
        /// <param name="plugin"></param>
        private void PluginFramework_OnPluginInstalled(IPluginFramework pluginFramework, IPlugin plugin)
        {

        }


        public void Execute()
        {
            this._pluginFramework.Start();

        }

    }
}
