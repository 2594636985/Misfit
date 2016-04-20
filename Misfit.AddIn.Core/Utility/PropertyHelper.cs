using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Misfit.AddIn.Utility
{
    internal static class PropertyHelper
    {
        public static AppDomain Domain
        {
            get
            {
                return AppDomain.CurrentDomain;
            }
        }

        public static void SetData(string name, object data)
        {
            Domain.SetData(name, data);
        }

        public static void SetData<T>(string name, T data)
        {
            SetData(name, data);
        }

        public static object GetData(string name)
        {
            return Domain.GetData(name);
        }

        public static T GetData<T>(string name)
        {
            return (T)GetData(name);
        }
    }
}
