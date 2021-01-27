using Reservation.Domains.Entities;
using Reservation.Domains.Envelopers;
using System.Collections.Generic;

namespace Reservation.Domains.Interfaces.Repositories
{
    /// <summary>
    /// Represent a contract of Contact Repository.
    /// </summary>
    public interface IContactRepository : IRepository<Contact>
    {
        Result<IEnumerable<Contact>> Get(string name);
    }
}
