using Reservation.Domains.Entities;
using Reservation.Domains.Envelopers;
using System.Collections;
using System.Collections.Generic;

namespace Reservation.Domains.Interfaces.Services
{
    /// <summary>
    /// Represent a contract of Contact Service (business).
    /// </summary>
    public interface IContactService : IService<Contact>
    {
        Result<IEnumerable<Contact>> Get(string name);
    }
}
