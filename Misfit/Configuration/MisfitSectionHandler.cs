using Misfit.Configuration.Elements;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace Misfit.Configuration
{
    /// <summary>
    /// MISFIT配置的节点处理类
    /// </summary>
    public class MisfitSectionHandler : ConfigurationSection
    {

        private const string DebugPropertyName = "Debug";
        private const string PluginsPropertyName = "Plugins";
        private const string VariablesPropertyName = "Variables";
        private const string AddInsRootPropertyName = "AddInsRoot";

        public const string DefaultSectionName = "Misfit";

        /// <summary>
        /// 模块域集合
        /// </summary>
        [ConfigurationProperty(DebugPropertyName, IsRequired = false, DefaultValue = "true")]
        public bool Debug
        {
            get
            {
                return Convert.ToBoolean(this[DebugPropertyName]);
            }
        }

        /// <summary>
        /// 模块域集合
        /// </summary>
        [ConfigurationProperty(VariablesPropertyName, IsRequired = false)]
        public VariableElementCollection Variables
        {
            get
            {
                return (VariableElementCollection)this[VariablesPropertyName];
            }
        }

        /// <summary>
        /// 模块域集合
        /// </summary>
        [ConfigurationProperty(PluginsPropertyName, IsRequired = false)]
        public PluginElementCollection Modules
        {
            get
            {
                return (PluginElementCollection)this[PluginsPropertyName];
            }
        }

        /// <summary>
        /// 子目录文件
        /// </summary>
        [ConfigurationProperty(AddInsRootPropertyName, IsRequired = true)]
        public AddInRootElement AddInsRoot
        {
            get
            {
                return (AddInRootElement)this[AddInsRootPropertyName];
            }
        }

        /// <summary>
        /// 解析对应配置文件的配置信息
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public static MisfitSectionHandler Deserialize(XmlReader reader)
        {
            if (reader == null)
                throw new ArgumentNullException("reader");

            reader.MoveToContent();

            if (reader.EOF)
            {
                throw new ConfigurationErrorsException();
            }
            var handler = new MisfitSectionHandler();
            handler.DeserializeElement(reader, false);
            return handler;
        }

        /// <summary>
        ///  解析对应配置文件的配置信息
        /// </summary>
        /// <param name="configurationFile"></param>
        /// <param name="configurationSection"></param>
        /// <returns></returns>
        public static MisfitSectionHandler Deserialize(string configurationFile)
        {
            if (string.IsNullOrWhiteSpace(configurationFile))
                throw new ArgumentNullException("configurationFile");

            if (!Path.IsPathRooted(configurationFile))
            {
                var configurationDirectory = Path.GetDirectoryName(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);
                configurationFile = Path.Combine(configurationDirectory, configurationFile);
            }

            if (!File.Exists(configurationFile))
                throw new FileNotFoundException(string.Format("没有找到对应的{0}配置文件", configurationFile));


            ExeConfigurationFileMap exeConfigurationFileMap = new ExeConfigurationFileMap();
            exeConfigurationFileMap.ExeConfigFilename = configurationFile;

            System.Configuration.Configuration configuration = null;

            try
            {
                configuration = ConfigurationManager.OpenMappedExeConfiguration(exeConfigurationFileMap, ConfigurationUserLevel.None);
            }
            catch (ConfigurationErrorsException)
            {
                using (var reader = new XmlTextReader(File.OpenRead(configurationFile)))
                {
                    return MisfitSectionHandler.Deserialize(reader);
                }
            }
            var handler = (MisfitSectionHandler)configuration.GetSection(DefaultSectionName);

            if (handler == null)
            {
                throw new ConfigurationErrorsException(string.Format("没有找到对应的 {0} 根节点", DefaultSectionName));
            }
            return handler;
        }


    }
}
