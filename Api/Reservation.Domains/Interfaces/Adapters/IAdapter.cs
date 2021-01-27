using Reservation.Domains.Envelopers;
using System.Collections.Generic;

namespace Reservation.Domains.Interfaces.Adapters
{
    /// <summary>
    /// Interface of Adapter pattern (complete references in https://www.dofactory.com/net/adapter-design-pattern).
    /// Basically, adapter will convert the interface of a class into another interface.
    /// </summary>
    /// <typeparam name="T">Generic Types</typeparam>
    public interface IAdapter<T>
        where T : class, new()
    {
        T New();
        Result<IEnumerable<T>> Adaptee(params object[] source);
    }
}
