using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Misfit.Plugins.Injection.Registration
{
    /// <summary>
    /// 注册容器
    /// </summary>
    internal class RegistrationContainer : IRegistrationContainer
    {
        private readonly object _lock = new object();
        private readonly List<RegistrationItem> _duplicateRegistrations;
        private readonly IDictionary<Type, RegistrationItem> _registrations;

        public IEnumerable<RegistrationItem> AllRegistrations
        {
            get
            {
                lock (this._lock)
                {
                    return new ReadOnlyCollection<RegistrationItem>(
                        this._registrations.Values.Concat(this._duplicateRegistrations).ToList());
                }
            }
        }

        internal RegistrationContainer()
        {
            this._duplicateRegistrations = new List<RegistrationItem>();
            this._registrations = new Dictionary<Type, RegistrationItem>();
        }

        public bool TryGetRegistration(Type contractType, out RegistrationItem registrationItem)
        {
            return this._registrations.TryGetValue(contractType, out registrationItem);
        }

        /// <summary>
        /// 用委托找注册项
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public RegistrationItem GetRegistration(Func<RegistrationItem, bool> predicate)
        {
            return this._registrations.Values.SingleOrDefault(predicate);
        }

        /// <summary>
        /// 判断是否注册过
        /// </summary>
        /// <param name="contractType"></param>
        /// <returns></returns>
        public bool IsRegistered(Type contractType)
        {
            return this.AllRegistrations.Any(registration => registration.ContractType == contractType);
        }

        /// <summary>
        /// 判断是否存在多注册
        /// </summary>
        /// <param name="contractType"></param>
        /// <returns></returns>
        public bool HasDuplicateRegistration(Type contractType)
        {
            return this._duplicateRegistrations.Any(registration => registration.ContractType == contractType);
        }

        /// <summary>
        /// 增加注册项
        /// </summary>
        /// <param name="registration"></param>
        public void AddRegistration(RegistrationItem registration)
        {
            lock (this._lock)
            {
                if (!this.IsRegistered(registration.ContractType))
                {
                    this._registrations.Add(registration.ContractType, registration);
                }
                else
                {
                    RegistrationItem duplicateItem;

                    this._registrations.TryGetValue(registration.ContractType, out duplicateItem);

                    if (duplicateItem != null)
                    {
                        this._duplicateRegistrations.Add(duplicateItem);
                        this.RemoveRegistration(duplicateItem.ContractType);
                    }

                    this._duplicateRegistrations.Add(registration);
                }
            }
        }

        /// <summary>
        /// 移除注册类型
        /// </summary>
        /// <param name="contractType"></param>
        public void RemoveRegistration(Type contractType)
        {
            lock (this._lock)
            {
                this._registrations.Remove(contractType);
            }
        }
    }
}