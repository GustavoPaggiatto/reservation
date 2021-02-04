using System.Collections;
using System.Collections.Generic;

namespace Reservation.Domains.Entities
{
    /// <summary>
    /// Represent a Contact type.
    /// </summary>
    public sealed class ContactType : BaseEntity
    {
        public string Description { get; set; }
        public IEnumerable<Contact> Contacts { get; set; }
    }
}
