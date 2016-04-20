using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Misfit.AddIn
{
    public class FrameworkUtil
    {
        private const string FrameworkUtilClassPath = "Misfit.AddIn.Framework.FrameworkUtil, Misfit.AddIn.Framework";

        private static MethodInfo createFilter;

        static FrameworkUtil()
        {
            Type type = Type.GetType(FrameworkUtilClassPath);
            createFilter = type.GetMethod("CreateFilter",
                    new Type[] { typeof(string) });
        }

        public FrameworkUtil()
        {

        }

        public static IFilter CreateFilter(string filter)
        {
            try
            {
                try
                {
                    return (IFilter)createFilter
                        .Invoke(null, new Object[] { filter });
                }
                catch (TargetException e)
                {
                    throw e.InnerException;
                }
            }
            catch (InvalidSyntaxException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
