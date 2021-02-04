using log4net;
using Microsoft.Extensions.Configuration;
using Reservation.Domains.Entities;
using Reservation.Domains.Envelopers;
using Reservation.Domains.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public Result<IEnumerable<Reserve>> Get(Contact contact)
        {
            this._logger.Debug("Starting method Get(Contact contact); Tier: Repository; Class: ReserveRepository.");

            var result = new Result<IEnumerable<Reserve>>();

            try
            {
                result.Content = this._entities.Where(r => r.ContactId == contact.Id);
            }
            catch (Exception ex)
            {
                this._logger.Error(ex);

                result.AddError($"{this._defaultExceptionText} getting Reservs of Contact, " +
                    $"try again or request technical team to view logs etc.");
            }
            finally
            {
                this._logger.Debug("Finishing method Get(Contact contact); Tier: Repository; Class: ReserveRepository.");
            }

            return result;
        }
    }
}
