using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Misfit.AddIn.Injection.Activation.Components
{
    /// <summary>
    /// 构选函数的选择器
    /// </summary>
    internal class ConstructorSelector : IConstructorSelector
    {
        public ConstructorInfo SelectConstructor(IEnumerable<ConstructorInfo> constructors, ResolutionContext resolutionContext)
        {
            var constructorsWithParameters = constructors.OrderByDescending(constructor => constructor.GetParameters().Length);

            ConstructorInfo finalConstructor = constructorsWithParameters.LastOrDefault();

            if (finalConstructor == null)
            {
                throw new ActivationException(string.Format("{0} 没有找到对应的构造函数"));
            }

            if (constructorsWithParameters.Count() == 1 && constructorsWithParameters.First().GetParameters().Length == 0)
            {
                return finalConstructor;
            }

            foreach (ConstructorInfo constructorCandidate in constructorsWithParameters)
            {
                ParameterInfo[] parameters = constructorCandidate.GetParameters();
                var dependencyParameters = parameters.Where(p => resolutionContext.RegistrationContainer.IsRegistered(p.ParameterType));

                if (resolutionContext.Arguments.CountOfAllArguments + resolutionContext.RuntimeArguments.CountOfAllArguments == 0 && parameters.Length == dependencyParameters.Count())
                {
                    finalConstructor = constructorCandidate;
                    break;
                }

                if (resolutionContext.Arguments.CountOfAllArguments > 0 || resolutionContext.RuntimeArguments.CountOfAllArguments > 0)
                {
                    if (resolutionContext.Arguments.CountOfAllArguments + resolutionContext.RuntimeArguments.CountOfAllArguments >= parameters.Count() - dependencyParameters.Count())
                    {
                        bool canSupply = true;

                        foreach (ParameterInfo parameter in parameters)
                        {
                            bool dependenciesCanSupplyValue = dependencyParameters.Contains(parameter);
                            bool argumentsCanSupplyValue = resolutionContext.Arguments.CanSupplyValue(parameter);
                            bool runtimeArgumentsCanSupplyValue = resolutionContext.RuntimeArguments.CanSupplyValue(parameter);

                            if (!(dependenciesCanSupplyValue || argumentsCanSupplyValue || runtimeArgumentsCanSupplyValue))
                            {
                                canSupply = false;
                            }
                        }

                        if (canSupply)
                        {
                            finalConstructor = constructorCandidate;
                            break;
                        }
                    }
                }
            }

            return finalConstructor;
        }
    }
}