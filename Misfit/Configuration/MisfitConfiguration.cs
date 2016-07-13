using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Misfit.Configuration
{
    /// <summary>
    /// 框架配置类
    /// </summary>
    public class MisfitConfiguration
    {
        public MisfitSectionHandler SectionHandler { private set; get; }

        public MisfitConfiguration()
        {
            this.SectionHandler = (MisfitSectionHandler)ConfigurationManager.GetSection(MisfitSectionHandler.DefaultSectionName);

            if (this.SectionHandler == null)
                throw new ConfigurationErrorsException(String.Format("没有到对应的配置元素节点{0}", MisfitSectionHandler.DefaultSectionName));
        }

        public MisfitConfiguration(string configurationFile)
        {
            this.SectionHandler = MisfitSectionHandler.Deserialize(configurationFile);
        }
    }
}
