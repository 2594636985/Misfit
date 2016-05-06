using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Misfit.AddIn.Cmd
{
    public interface ICommand
    {
        string CommandName { get; }

        object Execute(Dictionary<string, string> parameters);
    }
}
