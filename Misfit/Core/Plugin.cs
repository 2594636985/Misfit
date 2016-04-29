using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.Remoting;
using System.Xml;
using System.Xml.Serialization;
using Cramon.NetExtension.DynamicProxy;
using Misfit.AddIn;
using Misfit.Reflection;
using Misfit.Tracker;
using Misfit.Xml;

namespace Misfit.Core
{
    /// <summary>
    /// ÿ��Bundle����Ϣ
    /// </summary>
    public class Plugin : IPlugin
    {
        /// <summary>
        /// ģ��ID
        /// </summary>
        public int ModuleID { set; get; }

        /// <summary>
        /// ����ں�
        /// </summary>
        public IPluginFramework PluginFramework { set; get; }

        /// <summary>
        /// �������
        /// </summary>
        public string Name { set; get; }

        /// <summary>
        /// �ȼ�
        /// </summary>
        public int Level { set; get; }
        /// <summary>
        /// 
        /// </summary>
        public string Description { set; get; }


        /// <summary>
        /// �ڲ�����Ҫ�õ�����
        /// </summary>
        public string Location { set; get; }

        /// <summary>
        /// ��Ϊ
        /// </summary>
        public PluginAction Action { set; get; }

        /// <summary>
        /// �Ƿ�Ϊϵͳ���
        /// </summary>
        public bool IsSysPlugin { set; get; }


        public Plugin(string name, string location, string description, int level, string action, bool isSysPlugin)
        {
            this.Name = name;
            this.Description = description;
            this.Level = level;
            this.Location = location;
            this.IsSysPlugin = isSysPlugin;

            if (string.Compare(action, "Immediately", true) == 0)
                this.Action = PluginAction.Immediately;
            else
                this.Action = PluginAction.Delay;
        }
    }
}
