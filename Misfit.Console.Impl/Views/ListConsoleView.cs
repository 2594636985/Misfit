using System;
using System.Collections.Generic;
using System.Reflection;
using Misfit.AddIn;
using Misfit.Console.CommandLineParser;

namespace Misfit.Console.Views
{
    public class ListConsoleView
    {
        public ListConsoleView()
        {
            
        }

        public void ViewList(IBootstrapper shell)
        {
            WriteLine(" Framework is launched.");
            WriteLine(string.Empty);
            WriteLine(String.Format("{0}\t{1}\t\t{2}", "Id", "State", "Bundle"));
            WriteLine("");

            IBundleRepository bundles = shell.Framework.Bundles;
            for (int i = 0; i < bundles.Count; i++)
            {
                IBundle bundle = bundles[i];
                WriteLine(String.Format("{0}\t{1}\t{2}",
                    bundle.Id,
                    bundle.State.ToString().PadRight(10, ' '),
                    bundle.SymbolicName));
            }
        }

        public void Write(string value)
        {
            System.Console.Write(value);
        }

        public void WriteLine(string value)
        {
            System.Console.WriteLine(value);
        }
    }
}