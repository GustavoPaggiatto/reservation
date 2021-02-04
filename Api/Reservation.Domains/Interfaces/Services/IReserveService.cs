using Reservation.Domains.Entities;
using Reservation.Domains.Envelopers;
using System.Collections;
using System.Collections.Generic;

namespace Reservation.Domains.Interfaces.Services
{
    /// <summary>
    /// Represent a contract of Reserve Service (business).
    /// </summary>
    public interface IReserveService : IService<Reserve>
    {
        Result<IEnumerable<Reserve>> Get(Contact contact);
    }
}
