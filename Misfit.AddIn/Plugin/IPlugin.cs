using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Misfit.AddIn
{

    /// <summary>
    /// 模块
    /// </summary>
    public interface IPlugin
    {
        /// <summary>
        /// 模块ID
        /// </summary>
        int ModuleID { set; get; }

        /// <summary>
        /// 模块名
        /// </summary>
        string Name { set; get; }

        /// <summary>
        /// 模块等级
        /// </summary>
        int Level { set; get; }

        /// <summary>
        /// 模块描述
        /// </summary>
        string Description { set; get; }

        Version Version { set; get; }

        string SymbolicName { set; get; }

        PluginState PluginState { set; get; }

        Assembly Assembly { get; }

        IPluginContext BundleContext { get; }

        IPluginActivator[] Acitvators { get; }

        void Start();

        void Stop();
    }
}
