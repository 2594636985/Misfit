using Misfit.AomiServices.Inf;
using Misfit.Modulation.AddIn.Injection;
using Misfit.Modulation.AddIn.Serices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Misfit.HayasiServices
{
    public class TestService : MisfitAbsService, ITestService
    {
        public TestService(IMisfitContainer container)
            : base(container)
        {

        }

        public string Hello(string name)
        {
            return "hello hayasi " + name;
        }
    }
}
