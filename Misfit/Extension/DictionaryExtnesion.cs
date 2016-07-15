using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Misfit.Extension
{
    public static class DictionaryExtnesion
    {
        public static void AddRange(this Dictionary<string, string> srcDictionary, Dictionary<string, string> distDictionary)
        {
            foreach (string key in distDictionary.Keys)
            {
                srcDictionary.Add(key, distDictionary[key]);
            }
        }

        public static void AddRange(this Dictionary<string, object> srcDictionary, Dictionary<string, object> distDictionary)
        {
            foreach (string key in distDictionary.Keys)
            {
                srcDictionary.Add(key, distDictionary[key]);
            }
        }
    }
}
