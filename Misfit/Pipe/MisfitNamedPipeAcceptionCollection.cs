using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Misfit.Pipe
{
    public class MisfitNamedPipeAcceptionCollection : Dictionary<string, MisfitNamedPipeAcception>
    {
        public void Add(MisfitNamedPipeAcception misfitNamedPipeAcception)
        {
            this.Add(misfitNamedPipeAcception.Name, misfitNamedPipeAcception);
        }
    }
}
