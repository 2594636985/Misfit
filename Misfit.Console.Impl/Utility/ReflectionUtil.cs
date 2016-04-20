using System;
using System.Reflection;
using System.Collections.Generic;

namespace Misfit.Console.Utility
{
    public static class ReflectionUtil
    {
        // Methods

        public static string GetAssemblyFilename(Assembly assembly)
        {
            return assembly.CodeBase.Replace("file:///", "").Replace('/', '\\');
        }

        public static string GetAssemblyVersion(Assembly assembly)
        {
            foreach (string part in assembly.FullName.Split(','))
            {
                string trimmed = part.Trim();

                if (trimmed.StartsWith("Version="))
                    return trimmed.Substring(8);
            }

            return "0.0.0.0";
        }
    }
}