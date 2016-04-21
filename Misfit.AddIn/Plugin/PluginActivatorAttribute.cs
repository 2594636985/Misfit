using System;
using System.Reflection;

namespace Misfit.AddIn
{
    /// <summary>
    /// Attribute to be specified for each Egeye bundle to specify
    /// which class is the bundle activator.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class PluginActivatorAttribute : Attribute
    {
        public const string DefaultCategory = "Defalut";

        private string name;
        private string symbolicName;
        private string version;
        private string description;
        private string author;
        private string company;
        private string docURL;
        private string contactAddress;
        private string category;
        private string referencedAssemblies;
        private string exportAssembly;
        private string serviceComponent;

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        public string SymbolicName
        {
            get
            {
                return symbolicName;
            }
            set
            {
                symbolicName = value;
            }
        }

        public string Version
        {
            get
            {
                return version;
            }
            set
            {
                version = value;
            }
        }

        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                description = value;
            }
        }

        public string Author
        {
            get
            {
                return author;
            }
            set
            {
                author = value;
            }
        }

        public string Company
        {
            get
            {
                return company;
            }
            set
            {
                company = value;
            }
        }

        public string DocURL
        {
            get
            {
                return docURL;
            }
            set
            {
                docURL = value;
            }
        }

        public string ContactAddress
        {
            get
            {
                return contactAddress;
            }
            set
            {
                contactAddress = value;
            }
        }

        public string Category
        {
            get
            {
                return category;
            }
            set
            {
                category = value;
            }
        }

        public string ReferencedAssemblies
        {
            get
            {
                return referencedAssemblies;
            }
            set
            {
                referencedAssemblies = value;
            }
        }

        public string ExportAssembly
        {
            get
            {
                return exportAssembly;
            }
            set
            {
                exportAssembly = value;
            }
        }

        public string ServiceComponent
        {
            get
            {
                return serviceComponent;
            }
            set
            {
                serviceComponent = value;
            }
        }

        public PluginActivatorAttribute(string name)
        {
            this.name = name;
            this.symbolicName = string.Empty;
            this.version = "0.0.0.0";
            this.description = string.Empty;
            this.author = string.Empty;
            this.company = string.Empty;
            this.docURL = string.Empty;
            this.contactAddress = string.Empty;
            this.category = PluginActivatorAttribute.DefaultCategory;
            this.referencedAssemblies = "Egeye.AddIn";
            this.exportAssembly = string.Empty;
            this.serviceComponent = string.Empty;
        }
    }
}
