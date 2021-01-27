using log4net;
using Microsoft.Extensions.Configuration;
using Reservation.Domains.Entities;
using Reservation.Domains.Interfaces.Repositories;

namespace Reservation.Data
{
    /// <summary>
    /// Repository to manipulate Reserve instances.
    /// Here we use base resources only.
    /// </summary>
    public sealed class ReserveRepository : BaseRepository<Reserve>, IReserveRepository
    {
        public ReserveRepository(ILog logger, IConfiguration config) : base(logger, config)
        {
        }
    }
}
