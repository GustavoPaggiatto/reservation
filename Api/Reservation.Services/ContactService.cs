using log4net;
using Reservation.Domains.Entities;
using Reservation.Domains.Envelopers;
using Reservation.Domains.Interfaces.Repositories;
using Reservation.Domains.Interfaces.Services;
using System;
using System.Collections.Generic;

namespace Reservation.Services
{
    /// <summary>
    /// Business object to manipulate Contact instances.
    /// </summary>
    public sealed class ContactService : BaseService<Contact>, IContactService
    {
        public ContactService(IContactRepository repository, ILog logger) : base(repository, logger)
        {
        }

        /// <summary>
        /// Get all contacts that name attribute contains parameter value from repository.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Result<IEnumerable<Contact>> Get(string name)
        {
            this._logger.Debug("Starting method Get(name); Tier: Service; Class: ContractService.");

            var result = (this._repository as IContactRepository).Get(name);

            this._logger.Debug("Finishing method Get(name); Tier: Service; Class: ContractService.");

            return result;
        }

        /// <summary>
        /// Insert an contact in repository.
        /// Here we have a override due the necessary validations.
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public override Result Insert(Contact instance)
        {
            this._logger.Debug("Starting method Insert(); Tier: Service; Class: ContactService.");

            Result result = new Result();

            try
            {
                if (instance.Name == null || string.IsNullOrEmpty(instance.Name))
                {
                    result.AddError("Name was not informed.");
                    return result;
                }

                instance.Name = instance.Name.Trim();

                if (instance.BirthDate == DateTime.MinValue)
                {
                    result.AddError("Birthdate was not informed.");
                    return result;
                }

                if (instance.ContactTypeId == 0)
                {
                    result.AddError("Contact type was not informed.");
                    return result;
                }

                result = base.Insert(instance);
            }
            catch (Exception ex)
            {
                this._logger.Error(ex);

                result.AddError($"An error occurred while inserting Contact, {this._defaultExceptionText}");
            }
            finally
            {
                this._logger.Debug("Finishing method Insert(); Tier: Service; Class: ContactService.");
            }

            return result;
        }

        /// <summary>
        /// Update an contact in repository.
        /// Here we have a override due the necessary validations.
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public override Result Update(Contact instance)
        {
            this._logger.Debug("Starting method Update(); Tier: Service; Class: ContactService.");

            Result result = new Result();

            try
            {
                if (instance.Name == null || string.IsNullOrEmpty(instance.Name))
                {
                    result.AddError("Name was not informed.");
                    return result;
                }

                instance.Name = instance.Name.Trim();

                if (instance.BirthDate == DateTime.MinValue)
                {
                    result.AddError("Birthdate was not informed.");
                    return result;
                }

                if (instance.ContactTypeId == 0)
                {
                    result.AddError("Contact type was not informed.");
                    return result;
                }

                result = base.Update(instance);
            }
            catch (Exception ex)
            {
                this._logger.Error(ex);

                result.AddError($"An error occurred while updating Contact, {this._defaultExceptionText}");
            }
            finally
            {
                this._logger.Debug("Finishing method Update(); Tier: Service; Class: ContactService.");
            }

            return result;
        }
    }
}
