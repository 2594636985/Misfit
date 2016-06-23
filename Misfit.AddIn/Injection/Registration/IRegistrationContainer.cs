using System;
using System.Collections.Generic;

namespace Misfit.AddIn.Injection.Registration
{

    internal interface IRegistrationContainer
    {
        IEnumerable<RegistrationItem> AllRegistrations { get; }

        bool TryGetRegistration(Type contractType, out RegistrationItem registrationItem);

        RegistrationItem GetRegistration(Func<RegistrationItem, bool> predicate);

        /// <summary>
        /// ÅÐ¶ÏÊÇ·ñ×¢²á¹ý
        /// </summary>
        /// <param name="contractType"></param>
        /// <returns></returns>
        bool IsRegistered(Type contractType);

        /// <summary>
        /// ÅÐ¶ÏÊÇ·ñ´æÔÚ¶à×¢²á
        /// </summary>
        /// <param name="contractType"></param>
        /// <returns></returns>
        bool HasDuplicateRegistration(Type contractType);

     
        void AddRegistration(RegistrationItem registration);

   
        void RemoveRegistration(Type contractType);

    }
}