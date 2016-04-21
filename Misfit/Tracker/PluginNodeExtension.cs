using Misfit.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Misfit.Xml
{
    public static class PluginNodeExtension
    {
        public static Plugin ToBundle(this PluginNode pluginNode)
        {
            if (pluginNode == null)
                throw new ArgumentNullException("空对象是不能被转化的");

            return new Plugin(pluginNode.Name, pluginNode.SymbolicName, pluginNode.Description, pluginNode.Level, pluginNode.Version);
        }


        public static List<Plugin> ToBundleList(this PluginNodeList pluginNodeList)
        {
            if (pluginNodeList == null)
                throw new ArgumentNullException("空对象是不能被转化的");

            List<Plugin> bundleList = new List<Plugin>();

            foreach (PluginNode pluginNode in pluginNodeList)
            {
                bundleList.Add(pluginNode.ToBundle());
            }

            return bundleList;
        }



    }
}
