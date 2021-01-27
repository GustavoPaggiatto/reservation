using log4net;
using Microsoft.Extensions.Configuration;
using Reservation.Domains.Entities;
using Reservation.Domains.Interfaces.Repositories;

namespace Reservation.Data
{
    /// <summary>
    /// Repository to manipulate Contact type instances.
    /// Here we use base resources only.
    /// </summary>
    public sealed class ContactTypeRepository : BaseRepository<ContactType>, IContactTypeRepository
    {
        public ContactTypeRepository(ILog logger, IConfiguration config) : base(logger, config)
        {
        }
    }
}
