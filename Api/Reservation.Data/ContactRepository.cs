using log4net;
using Microsoft.Extensions.Configuration;
using Reservation.Domains.Entities;
using Reservation.Domains.Envelopers;
using Reservation.Domains.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Reservation.Data
{
    /// <summary>
    /// Repository to manipulate Contact instances.
    /// </summary>
    public sealed class ContactRepository : BaseRepository<Contact>, IContactRepository
    {
        public ContactRepository(ILog logger, IConfiguration config) : base(logger, config)
        {
        }

        /// <summary>
        /// Get contact by this name with 'LIKE' statement.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Result<IEnumerable<Contact>> Get(string name)
        {
            var result = new Result<IEnumerable<Contact>>();

            this._logger.Debug("Starting method Get(name); Tier: Repository; Class: ContractRepository.");

            try
            {
                result.Content = this._entities.Where(c => c.Name.Contains(name)).AsEnumerable();
            }
            catch (Exception ex)
            {
                this._logger.Error(ex);

                result.AddError($"{this._defaultExceptionText} getting Contacts, " +
                    $"try again or request technical team to view logs etc.");
            }
            finally
            {
                this._logger.Debug("Finishing method Get(name); Tier: Repository; Class: ContractRepository.");
            }

            return result;
        }

        /// <summary>
        /// Override of base method Get(ids).
        /// In this case I opted to use stored procedure bacause the project requirements need persistences with this database resource.
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public override Result<IEnumerable<Contact>> Get(IEnumerable<int> ids)
        {
            var result = new Result<IEnumerable<Contact>>();

            this._logger.Debug("Starting method Get(IEnumerable<int>); Tier: Repository; Class: ContractRepository.");

            try
            {
                string idsWithComma = string.Join(",", ids);

                result.Content = this._entities.FromSqlRaw<Contact>(
                    $"exec sp_get_contacts_by_reserveIds '{idsWithComma}'").AsEnumerable();
            }
            catch (Exception ex)
            {
                this._logger.Error(ex);

                result.AddError($"{this._defaultExceptionText} getting Contacts, " +
                    $"try again or request technical team to view logs etc.");
            }
            finally
            {
                this._logger.Debug("Finishing method Get(IEnumerable<int>); Tier: Repository; Class: ContractRepository.");
            }

            return result;
        }
    }
}
