using Misfit.AddIn.Injection;
using Misfit.AddIn.Serices;
using Misfit.AomiServices.Inf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Misfit.AomiServcies
{
    public class TestService : AddIn.Serices.MisfitAbsService, ITestService
    {
        public TestService(IMisfitContainer container)
            : base(container)
        {

        }

        public void Test()
        {

        }
    }
}
