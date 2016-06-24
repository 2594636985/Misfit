﻿using System;
using System.Reflection;

namespace Misfit.Modulation.AddIn.Injection.Activation.Components
{
    /// <summary>
    /// 参数收集器
    /// </summary>
    internal interface IArgumentCollector
    {
        object[] CollectArguments(Func<Type, object> dependencyResolver, ParameterInfo[] parameters, ResolutionContext resolutionContext);
    }
}