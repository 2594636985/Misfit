using Misfit.AddIn;
using Misfit.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Misfit.Xml
{
    public static class PluginNodeExtension
    {
        public static Plugin ToPlugin(this PluginNode pluginNode)
        {
            if (pluginNode == null)
                throw new ArgumentNullException("空对象是不能被转化的");

            return new Plugin()
            {
                Action = pluginNode.Action == "Immediately" ? PluginAction.Immediately : PluginAction.Delay,
                Activator = pluginNode.Activator,
                Description = pluginNode.Description,
                IsSysPlugin = false,
                Level = Convert.ToInt32(pluginNode.Level),
                Location = pluginNode.Location,
                Name = pluginNode.Name,
            };
        }


        public static List<Plugin> ToPluginList(this PluginNodeList pluginNodeList)
        {
            if (pluginNodeList == null)
                throw new ArgumentNullException("空对象是不能被转化的");

            List<Plugin> bundleList = new List<Plugin>();

            foreach (PluginNode pluginNode in pluginNodeList)
            {
                bundleList.Add(pluginNode.ToPlugin());
            }

            return bundleList;
        }



    }
}
