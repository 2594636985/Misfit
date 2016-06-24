using Misfit.Modulation.AddIn.Injection.Registration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Misfit.Modulation.AddIn.Injection
{
    public interface IMisfitContainerBuilder
    {
        /// <summary>
        /// 创建一个容器
        /// </summary>
        /// <returns></returns>
        IMisfitContainer Build();

        /// <summary>
        /// 通过一个实例来注册
        /// </summary>
        /// <typeparam name="TInstance"></typeparam>
        /// <param name="instance"></param>
        /// <returns></returns>

        IRegistrationBuilder Register<TInstance>(TInstance instance);

        /// <summary>
        /// 通过一个委托来注册
        /// </summary>
        /// <typeparam name="TContract"></typeparam>
        /// <param name="activatorFunction"></param>
        /// <returns></returns>
        IRegistrationBuilder Register<TContract>(Func<IMisfitContainer, TContract> activatorFunction);

        /// <summary>
        /// 通过实体类和接口类来注册
        /// </summary>
        /// <typeparam name="TContract"></typeparam>
        /// <typeparam name="TImplementation"></typeparam>
        /// <returns></returns>
        IRegistrationBuilder Register<TContract, TImplementation>() where TImplementation : TContract;
        /// <summary>
        /// 通过实体类和接口类的TYPE来注册
        /// </summary>
        /// <param name="typeOfContract"></param>
        /// <param name="typeOfImplementation"></param>
        /// <returns></returns>
        IRegistrationBuilder Register(Type typeOfContract, Type typeOfImplementation);
    }
}
