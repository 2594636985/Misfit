using System.Collections.Generic;
using System.Linq;
using System.Reflection;


namespace Misfit.Plugins.Injection.Registration
{
    internal class ArgumentContainer
    {
        internal object[] AnonymousArguments { get; set; }

        internal IDictionary<string, object> NamedArguments { get; set; }

        internal void AddToAnonymousArguments(IEnumerable<object> arguments)
        {
            object[] argumentsArray = arguments != null ? arguments.ToArray() : new object[] {};

            if (this.AnonymousArguments == null)
            {
                this.AnonymousArguments = argumentsArray;
            }
            else
            {
                this.AnonymousArguments = this.AnonymousArguments.Concat(argumentsArray).ToArray();
            }
        }

      
        internal void AddToNamedArguments(IDictionary<string, object> namedArguments)
        {
            if (this.NamedArguments == null)
            {
                this.NamedArguments = namedArguments;
            }
            else
            {
                this.NamedArguments = this.NamedArguments.Merge(namedArguments);
            }
        }

        internal int CountOfAllArguments
        {
            get
            {
                int count = 0;

                if (this.AnonymousArguments != null)
                {
                    count += this.AnonymousArguments.Length;
                }

                if (this.NamedArguments != null)
                {
                    count += this.NamedArguments.Count;
                }

                return count;
            }
        }

        internal bool CanSupplyValue(ParameterInfo parameter)
        {
            if (this.NamedArguments != null
                && this.NamedArguments.Keys.Any(k => k == parameter.Name)
                && this.NamedArguments.Values.Any(a => parameter.ParameterType.IsInstanceOfType(a)))
            {
                return true;
            }

            if (this.AnonymousArguments != null
                && this.AnonymousArguments.Any(a => parameter.ParameterType.IsInstanceOfType(a)))
            {
                return true;
            }

            return false;
        }
    }
}