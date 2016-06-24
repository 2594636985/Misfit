﻿using Misfit.Modulation.AddIn.Injection;
using Misfit.Modulation.AddIn.Serices;
using Misfit.AomiServices.Inf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Misfit.AomiServcies
{
    public class TestService : MisfitAbsService, ITestService
    {
        public TestService(IMisfitContainer container)
            : base(container)
        {

        }

        public string Hello(string name)
        {
            return "hello " + name;
        }
    }
}
