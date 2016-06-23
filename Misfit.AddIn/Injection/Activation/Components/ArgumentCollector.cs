using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Misfit.AddIn.Injection.Registration;

namespace Misfit.AddIn.Injection.Activation.Components
{
    /// <summary>
    /// 参数收集器
    /// </summary>
    internal class ArgumentCollector : IArgumentCollector
    {
        public object[] CollectArguments(Func<Type, object> dependencyResolver, ParameterInfo[] parameters, ResolutionContext resolutionContext)
        {
            var finalArguments = new List<object>();
            var dependencyParameters = parameters.Where(p => resolutionContext.RegistrationContainer.IsRegistered(p.ParameterType));

            Func<object, ParameterInfo, bool> argumentSelector = (argument, parameter) => parameter.ParameterType.IsInstanceOfType(argument);

            var runtimeArguments = resolutionContext.RuntimeArguments;
            var arguments = resolutionContext.Arguments;

            foreach (ParameterInfo parameter in parameters)
            {
                ParameterInfo localParameter = parameter;

                if (runtimeArguments.NamedArguments != null && runtimeArguments.NamedArguments.ContainsKey(parameter.Name))
                {
                    finalArguments.Add(runtimeArguments.NamedArguments[parameter.Name]);
                    continue;
                }

                if (runtimeArguments.AnonymousArguments != null && runtimeArguments.AnonymousArguments.Any(argument => argumentSelector(argument, localParameter)))
                {
                    finalArguments.Add(runtimeArguments.AnonymousArguments.FirstOrDefault(argument => argumentSelector(argument, localParameter)));
                    continue;
                }

                if (arguments.NamedArguments != null && arguments.NamedArguments.ContainsKey(parameter.Name))
                {
                    finalArguments.Add(arguments.NamedArguments[parameter.Name]);
                    continue;
                }

                if (arguments.AnonymousArguments != null && arguments.AnonymousArguments.Any(argument => argumentSelector(argument, localParameter)))
                {
                    finalArguments.Add(arguments.AnonymousArguments.FirstOrDefault(argument => argumentSelector(argument, localParameter)));
                    continue;
                }

                if (dependencyParameters.Contains(parameter))
                {
                    finalArguments.Add(dependencyResolver(parameter.ParameterType));
                    continue;
                }
            }

            return finalArguments.ToArray();
        }
    }
}