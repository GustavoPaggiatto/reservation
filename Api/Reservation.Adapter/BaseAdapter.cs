using Reservation.Domains.Envelopers;
using Reservation.Domains.Interfaces.Adapters;
using System;
using System.Collections.Generic;

namespace Reservation.Adapter
{
    /// <summary>
    /// Abstract class of Adapter pattern (complete references in https://www.dofactory.com/net/adapter-design-pattern).
    /// Basically, adapter convert the interface of a class into another interface.
    /// </summary>
    /// <typeparam name="T">Generic Types</typeparam>
    public abstract class BaseAdapter<T> : IAdapter<T>
        where T : class, new()
    {
        public abstract Result<IEnumerable<T>> Adaptee(params object[] source);

        /// <summary>
        /// Following low coupling (avoid use of 'new').
        /// </summary>
        /// <returns></returns>
        public T New()
        {
            return new T();
        }
    }
}
