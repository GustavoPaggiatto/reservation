using Reservation.Domains.Entities;
using Reservation.Domains.Envelopers;
using System.Collections.Generic;

namespace Reservation.Domains.Interfaces.Repositories
{
    /// <summary>
    /// Represent a contract of Reserve Repository.
    /// </summary>
    public interface IReserveRepository : IRepository<Reserve>
    {
        Result<IEnumerable<Reserve>> Get(Contact contact);
    }
}
