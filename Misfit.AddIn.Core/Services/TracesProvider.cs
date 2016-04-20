using System;
using Misfit.AddIn;

namespace Misfit.AddIn.Services
{
	/// <summary>
	/// Used to access the ITrace object.
	/// </summary>
    public class TracesProvider
    {
        private static ITrace output;

        private TracesProvider()
        {
        }
        
        public static ITrace TracesOutput
        {
            get
            {
                return output;
            }
        }

        public static void Initialize(ITrace trace)
        {
            output = trace;
            if(output == null)
            {
                throw new ArgumentNullException("Forbidden");
            }
        }
    }
}
