using log4net;
using Reservation.Domains.Entities;
using Reservation.Domains.Interfaces.Repositories;
using Reservation.Domains.Interfaces.Services;

namespace Reservation.Services
{
    /// <summary>
    /// Business object to manipulate Contact Type instances (use base resources).
    /// </summary>
    public sealed class ContactTypeService : BaseService<ContactType>, IContactTypeService
    {
        public ContactTypeService(IContactTypeRepository repository, ILog logger) : base(repository, logger)
        {
        }
    }
}
