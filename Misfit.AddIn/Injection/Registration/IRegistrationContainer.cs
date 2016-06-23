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
        /// �ж��Ƿ�ע���
        /// </summary>
        /// <param name="contractType"></param>
        /// <returns></returns>
        bool IsRegistered(Type contractType);

        /// <summary>
        /// �ж��Ƿ���ڶ�ע��
        /// </summary>
        /// <param name="contractType"></param>
        /// <returns></returns>
        bool HasDuplicateRegistration(Type contractType);

     
        void AddRegistration(RegistrationItem registration);

   
        void RemoveRegistration(Type contractType);

    }
}