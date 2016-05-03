using Misfit.AddIn;
using Misfit.Core;
using Misfit.Xml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.ComponentModel;
using System.Security.Policy;
using System.Threading;

namespace Misfit
{
    public class Bootstrapper
    {
        private AppDomain _debugDomain;
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
                string debugName = pDoc.ChildNodes.DebugName ?? "MisfitConsole";
                this._debugDomain = this._pluginFramework.CreateDomain("Sys-" + debugName);
                this._debugDomain.SetData("DebugName", debugName);
                this._debugDomain.DoCallBack(DebugDomainDoCallBack);
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
        /// 用于调试 日志窗体 的入口点
        /// </summary>
        private static void DebugDomainDoCallBack()
        {
            ThreadStart debugThreadStart = new ThreadStart(() =>
            {
                AppDomain domain = Thread.GetDomain();
                string debugName = Convert.ToString(domain.GetData("DebugName"));
                domain.ExecuteAssemblyByName(debugName);
            });
            Thread debugThread = new Thread(debugThreadStart);
            debugThread.SetApartmentState(ApartmentState.STA);
            debugThread.Start();
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
