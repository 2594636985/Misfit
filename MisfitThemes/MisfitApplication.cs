using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace MisfitThemes
{
    public class MisfitApplication : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            this.Resources.MergedDictionaries.Clear();
            this.Resources.MergedDictionaries.Add(GetResourceDictionary("Assets/ModernUI.xaml"));
            this.Resources.MergedDictionaries.Add(GetResourceDictionary("Assets/ModernUI.Light.xaml"));


        }

        private ResourceDictionary GetResourceDictionary(string uriString)
        {
            string assemblyName = typeof(MisfitApplication).Assembly.GetName().Name;
            ResourceDictionary resourceDictionary = new ResourceDictionary();
            resourceDictionary.Source = new Uri(string.Format("/{0};component/{1}", assemblyName, uriString), UriKind.Relative);
            return resourceDictionary;
        }
    }
}
