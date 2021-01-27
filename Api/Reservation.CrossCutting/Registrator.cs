using log4net;
using Microsoft.Extensions.Configuration;
using Reservation.Adapter;
using Reservation.Data;
using Reservation.Domains.Interfaces.Adapters;
using Reservation.Domains.Interfaces.Repositories;
using Reservation.Domains.Interfaces.Services;
using Reservation.Services;
using System;
using Unity;
using Unity.Injection;
using Unity.Lifetime;
using Unity.log4net;

namespace Reservation.CrossCutting
{
    /// <summary>
    /// Registrator is a concept for register all dependencies of projetc.
    /// It is on a separate layer to identify the implementations for each interface (other layers only see domain interfaces).
    /// Note that in this layer, we have references to data and business layer to make the link (these references are not necessary in the other layers to avoid breaking the IoC pattern).
    /// </summary>
    public sealed class Registrator
    {
        /// <summary>
        /// Static void method for manipulate de container DI.
        /// In this project we use the Unity Container(complete reference in http://unitycontainer.org/).
        /// </summary>
        /// <param name="container"></param>
        public static void Register(IUnityContainer container)
        {
            //Extensions...
            container.AddNewExtension<Log4NetExtension>();

            //Repositories...
            container.RegisterType<IContactRepository, ContactRepository>(
                new PerResolveLifetimeManager());
            container.RegisterType<IContactTypeRepository, ContactTypeRepository>(
                new PerResolveLifetimeManager());
            container.RegisterType<IReserveRepository, ReserveRepository>(
                new PerResolveLifetimeManager());

            //Services...
            container.RegisterType<IContactService, ContactService>(
                new PerResolveLifetimeManager());
            container.RegisterType<IContactTypeService, ContactTypeService>(
                new PerResolveLifetimeManager());
            container.RegisterType<IReserveService, ReserveService>(
                new PerResolveLifetimeManager());

            //Adapters...
            container.RegisterType<IReserveContactAdapter, ReserveContactAdapter>(
                new PerResolveLifetimeManager());
        }
    }
}
